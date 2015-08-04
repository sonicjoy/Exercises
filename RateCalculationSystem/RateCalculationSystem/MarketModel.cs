using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RateCalculationSystem
{
    public class MarketModel
    {
        private List<Lender> _lenderPool;
        public List<Lender> LenderPool 
        {
            get
            {
                if (_lenderPool == null) _lenderPool = new List<Lender>();
                return _lenderPool;
            }
        }

        public MarketModel(string fileName, bool hasHeaderRow) 
        {
            var filePath = Environment.CurrentDirectory + "\\" + fileName;
            string[] csvRows = System.IO.File.ReadAllLines(filePath);
            BuildLenderPoolFrom(csvRows, hasHeaderRow);          
        }

        private void BuildLenderPoolFrom(string[] csvRows, bool hasHeaderRow)
        {
            var skipRow = hasHeaderRow;
            foreach (string csvRow in csvRows)
            {
                //skip header row
                if (skipRow)
                {
                    skipRow = false;
                    continue;
                }

                var fields = csvRow.Split(',');
                double rate = 0;
                double amount = 0;
                var lender = new Lender();
                lender.Name = fields[0].ToString();
                Double.TryParse(fields[1], out rate);
                lender.Rate = rate;
                Double.TryParse(fields[2], out amount);
                lender.Amount = amount;

                LenderPool.Add(lender);
            }
        }

        [Serializable]
        public class Lender
        {
            public String Name { get; set; }
            public Double Rate { get; set; }
            public Double Amount { get; set; }
        }
    }
}
