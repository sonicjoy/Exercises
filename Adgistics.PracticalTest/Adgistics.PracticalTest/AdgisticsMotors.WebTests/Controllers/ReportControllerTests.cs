using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdgisticsMotorsReport.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdgisticsMotorsReport.Web.Tests
{
    [TestClass()]
    public class ReportControllerTests
    {
        ReportController controller;
        [TestInitialize]
        public void PrepareReportsTest()
        {
            controller = new ReportController();
            controller.PrepareReports();
        }

        [TestMethod()]
        public void GetTopPerformerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetLowStockerTest()
        {
            Assert.Fail();
        }
    }
}