using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;
using AdgisticsMotorsReport.Utils.Threading;
using System.Threading;

namespace AdgisticsMotorsReport.Web
{
    public class ReportController : ApiController
    {
        static List<DealershipData> dealershipDataSet;

        [HttpGet]
        public void PrepareReports()
        {
            var dealershipList = GetDealershipList();
            var outstanding = new List<DataCollector>();
            var processing = new List<DataCollector>();
            var failed = new List<DataCollector>();
            var status = new QueueStatus(outstanding, processing, failed);
            CollectDealershipData(dealershipList, out status);    
        }

        [HttpGet]
        public List<DealershipData> GetTopPerformer(int topNumber = 100)
        {
            var topPerformer = new List<DealershipData>();
            if (dealershipDataSet.Any())
                topPerformer = dealershipDataSet.OrderByDescending(d => d.TotalSales).Take(topNumber).ToList();
            return topPerformer;
        }

        [HttpGet]
        public List<DealershipData> GetLowStocker(int thresholdNumber = 10)
        {
            var lowStocker = new List<DealershipData>();
            if (dealershipDataSet.Any())
                lowStocker = dealershipDataSet.FindAll(d => d.AvailableStock < thresholdNumber);
            return lowStocker;
        }

        private void CollectDealershipData(List<string[]> dealershipList, out QueueStatus status)
        {
            dealershipDataSet = new List<DealershipData>();
            var workerThreads = 50;
            using (var worker = new BackgroundWorkerQueue(workerThreads))
            {
                foreach (var dealership in dealershipList)
                {
                    var work = new DataCollector(worker, dealership[0], new Uri(dealership[1]));
                    worker.Enqueue(work);
                }

                status = worker.Status();
                while(status.Backlog.Any())
                {
                    Thread.Sleep(1000);
                    status = worker.Status();
                }
                worker.Stop();
                worker.ClearErrors();
            }
        }

        private List<string[]> GetDealershipList()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/DealershipsList.txt");
            var dealershipFile = File.ReadAllLines(filePath);
            return dealershipFile.Select(d => d.Split(',')).ToList();
        }

        private sealed class DataCollector : WorkBase
        {
            private readonly string _id;
            private readonly Uri _uri;
            private readonly BackgroundWorkerQueue _workerQueue;

            public DataCollector(BackgroundWorkerQueue workerQueue, string id, Uri uri)
            {
                _workerQueue = workerQueue;
                _id = id;
                _uri = uri;
            }

            public override void Process()
            {
                var service = new DealershipService();
                try
                {
                    var _dealershipData = service.GetDealershipData(_id, _uri);
                    dealershipDataSet.Add(_dealershipData);
                }
                catch(HttpException)
                {
                    this._workerQueue.ReAddFailed(new List<DataCollector> { this });
                }
            }
        }

        private abstract class WorkBase : IWork
        {
            private static int uniqueIdCounter = 0;

            public WorkBase()
            {
                this.Id = uniqueIdCounter++;
            }

            protected int Id { get; set; }

            public abstract void Process();

            private bool Equals(WorkBase other)
            {
                return this.Id == other.Id;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }
                return obj is WorkBase && this.Equals((WorkBase)obj);
            }

            public override int GetHashCode()
            {
                return this.Id;
            }
        }
    }
}