using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace RateCalculationSystem.Test
{
    [TestClass]
    public class RateCalculatorUnitTest
    {
        private List<MarketModel.Lender> mockLenderPool = new List<MarketModel.Lender>();
        private int mockRequestedAmount = 1000;
        private int mockNumberOfRepayment = 36;

        [TestInitialize]
        public void Initialise_Test_Data()
        {//total availabe 4730; be very cautious when you want to modify the mock pool, some tests are depending on the exact numbers.
            mockLenderPool.Add(new MarketModel.Lender { Name = "A", Rate = 0.05, Amount = 400 });
            mockLenderPool.Add(new MarketModel.Lender { Name = "B", Rate = 0.04, Amount = 1000 });
            mockLenderPool.Add(new MarketModel.Lender { Name = "C", Rate = 0.055, Amount = 500 });
            mockLenderPool.Add(new MarketModel.Lender { Name = "D", Rate = 0.08, Amount = 800 });
            mockLenderPool.Add(new MarketModel.Lender { Name = "E", Rate = 0.075, Amount = 1080 });
            mockLenderPool.Add(new MarketModel.Lender { Name = "F", Rate = 0.06, Amount = 950 });
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Lender_Pool_Is_Empty_Throws_Error()
        {
            var emptylenderPool = new List<RateCalculationSystem.MarketModel.Lender>();
            var quote = RateCalculator.GetQuote(emptylenderPool, mockRequestedAmount, mockNumberOfRepayment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void If_Number_Of_Repayment_Is_0_Throws_Error()
        {
            var numberOfRepayment1 = 0;
            var quote1 = RateCalculator.GetQuote(mockLenderPool, mockRequestedAmount, numberOfRepayment1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void If_Number_Of_Repayment_Is_Negative_Throws_Error()
        {
            var numberOfRepayment2 = -1;
            var quote2 = RateCalculator.GetQuote(mockLenderPool, mockRequestedAmount, numberOfRepayment2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void If_Requested_Amount_Is_Less_Than_1000_After_Rounding_Throws_Error()
        {
            var requestedAmount = 949;
            var quote = RateCalculator.GetQuote(mockLenderPool, requestedAmount, mockNumberOfRepayment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void If_Requested_Amount_Is_More_Than_15000_After_Rounding_Throws_Error()
        {
            var requestedAmount = 15050;
            var quote = RateCalculator.GetQuote(mockLenderPool, requestedAmount, mockNumberOfRepayment);
        }

        [TestMethod]
        public void Valid_Requested_Amount_Should_Be_Rounded_To_Nearest_100_Amount()
        {
            var requestedAmount1 = 1234;
            var quote1 = RateCalculator.GetQuote(mockLenderPool, requestedAmount1, mockNumberOfRepayment);
            Assert.AreEqual<int>(1200, quote1.LoanAmount);

            var requestedAmount2 = 1251;
            var quote2 = RateCalculator.GetQuote(mockLenderPool, requestedAmount2, mockNumberOfRepayment);
            Assert.AreEqual<int>(1300, quote2.LoanAmount);

            var requestedAmount3 = 1400;
            var quote3 = RateCalculator.GetQuote(mockLenderPool, requestedAmount3, mockNumberOfRepayment);
            Assert.AreEqual<int>(1400, quote3.LoanAmount);
        }

        [TestMethod]
        public void Requested_Amount_Is_Under_The_Available_Amount_After_Rounding()
        {
            var requestedAmount = 4749;
            var quote = RateCalculator.GetQuote(mockLenderPool, requestedAmount, mockNumberOfRepayment);
            Assert.AreEqual<int>(4700, quote.LoanAmount);
        }

        [TestMethod]
        public void Requested_Amount_Less_Than_1000_Can_Be_Rounded_To_1000()
        {
            var requestedAmount = 950;
            var quote = RateCalculator.GetQuote(mockLenderPool, requestedAmount, mockNumberOfRepayment);
            Assert.AreEqual<int>(1000, quote.LoanAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Requested_Amount_Is_More_Than_Available_Amount_After_Rounding_Throws_Error()
        {   //This should throw ApplicationException because rounded loan 15000 is more than the available amount in the mock pool
            //But it should not throw ArgumentOutOfRangeException as it is rounded back to the valid range.
            var requestedAmount = 15049;
            var quote = RateCalculator.GetQuote(mockLenderPool, requestedAmount, mockNumberOfRepayment);
        }

        [TestMethod]
        public void If_There_Is_Sufficient_Fund()
        {//total available fund in the mock pool is 4730
            var requestedAmount1 = 1234;
            var quote1 = RateCalculator.GetQuote(mockLenderPool, requestedAmount1, mockNumberOfRepayment);
            Assert.AreEqual<int>(1200, quote1.LoanAmount);

            var requestedAmount2 = 4749; //an edge case as the total available in the pool is 4730, but this will be rounded down to 4700, so still valid
            var quote2 = RateCalculator.GetQuote(mockLenderPool, requestedAmount2, mockNumberOfRepayment);
            Assert.AreEqual<int>(4700, quote2.LoanAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Insufficient_Fund_Throws_Error()
        {
            var requestedAmount1 = 15000; //this is more than the money in the mock pool 4730
            var quote1 = RateCalculator.GetQuote(mockLenderPool, requestedAmount1, mockNumberOfRepayment);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void If_Insufficient_Fund_After_Rounded_Throws_Error()
        {
            var requestedAmount2 = 4750; //this should be rounded to 4800 before checking the total available
            var quote2 = RateCalculator.GetQuote(mockLenderPool, requestedAmount2, mockNumberOfRepayment);
        }

        [TestMethod]
        public void Rate_Should_Be_Lowest_Possible_From_The_Pool_For_Given_Loan()
        {
            //Single lender: entirely from lender B whose rate is the lowest in the mock pool, so it is B'r rate
            var requestedAmount1 = 1000;
            var quote1 = RateCalculator.GetQuote(mockLenderPool, requestedAmount1, mockNumberOfRepayment);
            Assert.AreEqual<double>(0.04, quote1.Rate); 

            //More than 1 lender with just right amount: A and B, requested 1400, the rate should be (60.0 / 1400.0)
            var requestedAmount2 = 1400;
            var quote2 = RateCalculator.GetQuote(mockLenderPool, requestedAmount2, mockNumberOfRepayment);
            Assert.AreEqual<double>((60.0 / requestedAmount2), quote2.Rate);

            //More than 1 lender with redundant amount: A, B and C, requested 1500, the rate should be (65.5 / 1500.0)
            var requestedAmount3 = 1500;
            var quote3 = RateCalculator.GetQuote(mockLenderPool, requestedAmount3, mockNumberOfRepayment);
            Assert.AreEqual<double>((65.5 / requestedAmount3), quote3.Rate);
        }

        [TestMethod]
        public void Monthly_Repayment_Should_Be_Calculated_Correctly()
        {
            //Test 1: 1000 loan, for 36 monthes. Rate should be 0.04, and the repayment should be 29.52
            var requestedAmount1 = 1000;
            var numberOfRepayment1 = 36;
            var quote1 = RateCalculator.GetQuote(mockLenderPool, requestedAmount1, numberOfRepayment1);
            Assert.AreEqual<string>("29.52", quote1.RepaymentAmount.ToString("0.00", CultureInfo.InvariantCulture));

            //Test 2: 1400 loan, for 36 monthes. Rate should be 0.043, and the repayment should be 41.51
            var requestedAmount2 = 1400;
            var numberOfRepayment2 = 36;
            var quote2 = RateCalculator.GetQuote(mockLenderPool, requestedAmount2, numberOfRepayment2);
            Assert.AreEqual<string>("41.51", quote2.RepaymentAmount.ToString("0.00", CultureInfo.InvariantCulture));

            //Test 3: 1500 loan, for 24 monthes. Rate should be 0.044, and the repayment should be 65.38
            var requestedAmount3 = 1500;
            var numberOfRepayment3 = 24;
            var quote3 = RateCalculator.GetQuote(mockLenderPool, requestedAmount3, numberOfRepayment3);
            Assert.AreEqual<string>("65.38", quote3.RepaymentAmount.ToString("0.00", CultureInfo.InvariantCulture)); 

        }
    }

    [TestClass]
    public class QuoteUnitTest
    {
        [TestMethod]
        public void Test_Quote_Default_Values()
        {
            var quote = new Quote();
            Assert.AreEqual<int>(1000, quote.LoanAmount);
            Assert.AreEqual<int>(36, quote.NumberOfRepayments);
            Assert.AreEqual<double>(0.0, quote.Rate);
            Assert.AreEqual<double>(0.00, quote.RepaymentAmount);
        }

        [TestMethod]
        public void Total_Repayment_Equals_Repayment_Amount_Times_Number_Of_Repayments()
        {
            //test with default values
            var quote1 = new Quote();
            Assert.AreEqual<double>(0, quote1.TotalRepayment);
        }
    }
}
