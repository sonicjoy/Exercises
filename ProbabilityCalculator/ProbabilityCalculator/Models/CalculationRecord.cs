using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProbabilityCalculator.Controllers
{
    [Serializable]
    public class CalculationRecord
    {
        public DateTime RecordDateTime;
        public String FunctionType;
        public Double ProbabilityA;
        public Double ProbabilityB;
        public Double Result;
    }
}
