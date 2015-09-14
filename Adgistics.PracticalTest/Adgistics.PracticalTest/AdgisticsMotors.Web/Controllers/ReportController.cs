using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;
using AdgisticsMotorsReport.Utils.Threading;

namespace AdgisticsMotorsReport.Web
{
    public class ReportController : ApiController
    {
        static List<DealershipData> dealershipDataSet;
        static List<DataCollector> failedWork;

        [HttpGet]
        public void PrepareReports()
        { 
            var dealershipList = GetDealershipList();
            dealershipDataSet = GetDealershipData(dealershipList);         
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

        private List<DealershipData> GetDealershipData(List<string[]> dealershipList)
        {
            dealershipDataSet = new List<DealershipData>();
            var service = new DealershipService();
            var retryList = dealershipList.Select(d => (string[])d.Clone()).ToList() ;
            while(retryList.Any())
            {
                var dealership = retryList.FirstOrDefault();
                retryList.Remove(dealership);
                try
                {
                    var dealershipData = service.GetDealershipData(dealership[0], new Uri(dealership[1]));
                    dealershipDataSet.Add(dealershipData);
                }
                catch(HttpException)
                {
                    retryList.Add(dealership);
                }
            }

            return dealershipDataSet;
        }

        private List<string[]> GetDealershipList()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/DealershipsList.txt");
            var dealershipFile = File.ReadAllLines(filePath);
            return dealershipFile.Select(d => d.Split(',')).ToList();
        }

        private sealed class DataCollector : WorkBase
        {
            string _id = string.Empty;
            Uri _uri;
            private readonly BackgroundWorkerQueue _workerQueue;
            DealershipData _dealershipData;

            DataCollector(BackgroundWorkerQueue workerQueue, string id, Uri uri)
            {
                _workerQueue = workerQueue;
                this._workerQueue.WorkSucceeded += this.OnWorkSucceed;
                this._workerQueue.WorkFailed += this.OnWorkFailed;
                _id = id;
                _uri = uri;
            }

            public override void Process()
            {
                var service = new DealershipService();
                try
                {
                    _dealershipData = service.GetDealershipData(_id, _uri);
                }
                catch(HttpException ex)
                {
                    throw new ApplicationException(ex.Message, ex.InnerException);
                }
            }

            void OnWorkSucceed(object sender, WorkSucceededProcessingEventArgs e)
            {
                dealershipDataSet.Add(_dealershipData);
            }

            void OnWorkFailed(object sender, WorkFailedProcessingEventArgs e)
            {
                this._workerQueue.ReAddFailed(new List<DataCollector> { this });
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