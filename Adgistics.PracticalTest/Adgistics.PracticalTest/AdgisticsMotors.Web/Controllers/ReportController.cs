using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;
using AdgisticsMotorsReport.Utils.Threading;
using System.Threading;
using AdgisticsMotorsReport.Web.Hubs;

namespace AdgisticsMotorsReport.Web
{
    public class ReportController : ApiController
    {
        static List<DealershipData> dealershipDataSet;
        private List<DealershipData> dummyDataList;
        private readonly static object readerLock = new object();

        [HttpGet]
        public void PrepareReports()
        {
            var dealershipList = GetDealershipList();
            var outstanding = new List<DataCollector>();
            var processing = new List<DataCollector>();
            var failed = new List<DataCollector>();

            var dataHub = new DataHub();
            CollectDealershipData(dealershipList, dataHub);
        }

        [HttpGet]
        public List<DealershipData> GetReport(string reportType)
        {
            if (reportType == "top_performer") return GetTopPerformer();
            if (reportType == "low_stocker") return GetLowStocker();
            return new List<DealershipData>();
        }

        [HttpGet]
        public List<DealershipData> GetTopPerformer(int topNumber = 100)
        {
            var topPerformer = new List<DealershipData>();
            if (dealershipDataSet != null && dealershipDataSet.Any())
                topPerformer = dealershipDataSet.OrderByDescending(d => d.TotalSales).Take(topNumber).ToList();
            else //for testing
            {
                GenerateDummyDataList(topNumber);
                topPerformer = dummyDataList.OrderByDescending(d => d.TotalSales).Take(topNumber).ToList();
            }
            return topPerformer;
        }

        [HttpGet]
        public List<DealershipData> GetLowStocker(int thresholdNumber = 10)
        {
            var lowStocker = new List<DealershipData>();
            if (dealershipDataSet != null && dealershipDataSet.Any())
                lowStocker = dealershipDataSet.FindAll(d => d.AvailableStock < thresholdNumber);
            else //for testing
            {
                GenerateDummyDataList(100);
                lowStocker = dummyDataList.FindAll(d => d.AvailableStock < thresholdNumber);
            }
            return lowStocker;
        }

        private void GenerateDummyDataList(int listSize)
        {
            dummyDataList = new List<DealershipData>();
            for(var i = 0; i < listSize; i++)
            {
                var dealershipData = new DealershipData { DealershipIdentifier = i.ToString(), AvailableStock = i, TotalSales = i * 10000 };
                dummyDataList.Add(dealershipData);
            }
        }

        private void CollectDealershipData(List<string[]> dealershipList, DataHub dataHub)
        {
            dealershipDataSet = new List<DealershipData>();
            var workerThreads = 99;
            using (var worker = new BackgroundWorkerQueue(workerThreads))
            {
                foreach (var dealership in dealershipList)
                {
                    var work = new DataCollector(dealership[0], new Uri(dealership[1]));
                    worker.Enqueue(work);
                }

                dataHub.SendTotal(dealershipList.Count);
                var completed = 0;
                var status = worker.Status();
                do
                {
                    if (status.Failed.Any())
                        worker.ReAddFailed(status.Failed);
                    Thread.Sleep(1000);
                    status = worker.Status();

                    if (Monitor.TryEnter(readerLock, 100))
                    {
                        try
                        {
                            completed = dealershipDataSet.Count;
                        }
                        finally
                        {
                            Monitor.Exit(readerLock);
                        }
                    }

                    dataHub.SendProgress(completed, status.Processing.Count(), status.Failed.Count());
                } while (status.Backlog.Any() || status.Processing.Any() || status.Failed.Any());
                worker.Stop();
                worker.ClearErrors();
                dataHub.CompleteDataCollection();
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
            private DealershipData _dealershipData;
            private readonly static object writerLock = new object();

            public DataCollector(string id, Uri uri)
            {
                _id = id;
                _uri = uri;
            }

            public override void Process()
            {
                var service = new DealershipService();
                _dealershipData = service.GetDealershipData(_id, _uri);
                if (Monitor.TryEnter(writerLock, 300))
                {
                    try
                    {
                        dealershipDataSet.Add(_dealershipData);
                    }
                    finally
                    {
                        Monitor.Exit(writerLock);
                    }
                }
                else
                {
                    throw new TimeoutException();
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