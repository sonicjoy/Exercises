using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RateCalculationSystem;

namespace RateCalculationSystem.Test
{
    [TestClass]
    public class ControllerUnitTest
    { 
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Initialising_Controller_With_All_Null_Arguments_Throws_Error()
        {
            //both null arguments
            var args = new string[2];
            var controller = new Controller(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Initialising_Controller_With_One_Null_Argument_Throws_Error()
        {
            //null arguments
            var args = new string[2];
            args[0] = string.Empty;
            var controller = new Controller(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Initialising_Controller_With_String_As_Second_Argument_Throws_Error()
        {
            //null arguments
            var args = new string[2];
            args[0] = string.Empty;
            args[1] = string.Empty;
            var controller = new Controller(args);
        }
    }
}
