using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem;

namespace RateCalculationSystem.Test
{
    [TestClass]
    public class MarketDataModelTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void If_File_Not_Found_In_Current_Assembly_Location_Throws_Error()
        {
            var fileName = Guid.NewGuid().ToString(); //make sure the file doesn't happen to exist
            var marketData = new MarketModel(fileName, true);
        }
    }
}
