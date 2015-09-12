using System.Linq;
using System.Web.Http;
using System.IO;
using System.Web;
using System;
using System.Collections.Generic;

namespace AdgisticsMotorsReport.Web
{
    public class ReportController : ApiController
    {
        List<DealershipData> dealershipData;

        [HttpGet]
        public void PrepareReports()
        { 
            var dealershipList = GetDealershipList();
            dealershipData = GetDealershipData(dealershipList);         
        }

        [HttpGet]
        public List<DealershipData> GetTopPerformer(int topNumber = 100)
        {
            var topPerformer = new List<DealershipData>();
            if (dealershipData.Any())
                topPerformer = dealershipData.OrderByDescending(d => d.TotalSales).Take(topNumber).ToList();
            return topPerformer;
        }

        [HttpGet]
        public List<DealershipData> GetLowStocker(int thresholdNumber = 10)
        {
            var lowStocker = new List<DealershipData>();
            if (dealershipData.Any())
                lowStocker = dealershipData.FindAll(d => d.AvailableStock < thresholdNumber);
            return lowStocker;
        }

        private List<DealershipData> GetDealershipData(List<string[]> dealershipList)
        {
            throw new NotImplementedException();
        }

        private List<string[]> GetDealershipList()
        {
            var filePath = HttpContext.Current.Server.MapPath("~/App_Data/DealershipsList.txt");
            var dealershipFile = File.ReadAllLines(filePath);
            return dealershipFile.Select(d => d.Split(',')).ToList();
        }
    }
}