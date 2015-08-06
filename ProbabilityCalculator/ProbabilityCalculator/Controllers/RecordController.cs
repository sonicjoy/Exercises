using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProbabilityCalculator.Controllers
{
    public class RecordController : ApiController
    {
        [HttpPost]
        public bool SaveCalculation(CalculationRecord calculation)
        {
            //BUG: The data post back are null data at the moment, more investigation needed.
            //TODO: Insert code here to save the calculation in some storage
            return true;
        }
    }
}
