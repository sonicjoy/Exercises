using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculationSystem
{
    public static class RateCalculator
    {
        static int MIN_LOAN_AMOUNT = 1000;
        static int MAX_LOAN_AMOUNT = 15000;
        static int ROUNDING_BASE = 100;

        /// <summary>
        /// The method will calculate the lowest rate from the lender pool and workout the repayment based on the number of repayments
        /// </summary>
        /// <param name="lenderPool">A list of Lender objects.</param>
        /// <param name="requestedAmount">As int, cannont be negative</param>
        /// <param name="numberOfRepayments">As int, cannot be negative.</param>
        /// <returns>Quote object.</returns>
        public static Quote GetQuote(List<MarketModel.Lender> lenderPool, int requestedAmount, int numberOfRepayments)
        {
            if (!lenderPool.Any()) throw new ApplicationException("It is not possible to provide a quote at this time.");
            if (numberOfRepayments < 1) throw new ArgumentOutOfRangeException("The number of repayment cannot be less than 1.");

            var quote = new Quote();
            quote.LoanAmount = GetRoundedAmount(requestedAmount);
                
            //check rounded LoanAmount rather than original requestedAmount to give user more chance
            if (quote.LoanAmount > MAX_LOAN_AMOUNT) throw new ArgumentOutOfRangeException("The maximum loan amount we offer is " + MAX_LOAN_AMOUNT.ToString("C") + ".");
            else if (quote.LoanAmount > lenderPool.Sum(a => a.Amount)) throw new ApplicationException("It is not possible to provide a quote at this time.");
            else if (quote.LoanAmount < MIN_LOAN_AMOUNT) throw new ArgumentOutOfRangeException("The minimum loan amount we offer is " + MIN_LOAN_AMOUNT.ToString("C") + ".");

            quote.Rate = GetLowestRate(lenderPool, quote.LoanAmount);
            quote.RepaymentAmount = GetRepaymentAmount(quote.LoanAmount, quote.Rate, numberOfRepayments);
            return quote;
        }

        private static int GetRoundedAmount(int requestedAmount)
        {
            var roundedAmount = 0;
            roundedAmount = (int)Math.Round((decimal)requestedAmount / ROUNDING_BASE, MidpointRounding.AwayFromZero) * ROUNDING_BASE;
            return roundedAmount;
        }

        private static double GetLowestRate(List<MarketModel.Lender> lenderPool, int loanAmount)
        {
            var availableFund = 0.00;
            var optimumPool = new List<MarketModel.Lender>();
            foreach (var lender in lenderPool.OrderBy(l => l.Rate))
            {
                availableFund += lender.Amount;
                var lenderClone = new MarketModel.Lender { Name = lender.Name, Rate = lender.Rate, Amount = lender.Amount };
                optimumPool.Add(lenderClone);
                if (availableFund >= loanAmount)
                {
                    lenderClone.Amount -= availableFund - loanAmount; //removed redundant amount from the last added lender
                    break;
                }
            }          
            if ((int)optimumPool.Sum(l => l.Amount) != loanAmount) throw new ArithmeticException("Something is wrong"); //check they are equal just in case

            var blendedRate = 0.0;
            blendedRate = optimumPool.Sum(l => l.Amount * l.Rate) / loanAmount;
            return blendedRate;
        }

        private static double GetRepaymentAmount(int loanAmount, double rate, int numberOfRepayments)
        {
            var repayment = 0.0;
            repayment = loanAmount * rate / 12 / (1 - Math.Pow((1 + rate / 12), (-numberOfRepayments)));
            return repayment;
        }
    }

    [Serializable]
    public class Quote
    {
        private int _loanAmount = 1000;
        public int LoanAmount
        {
            get
            {
                return _loanAmount;
            }
            internal set
            {
                _loanAmount = value;
            }
        }
        private int _numberOfRepayment = 36;
        public int NumberOfRepayments
        {
            get
            {
                return _numberOfRepayment;
            }
            internal set
            {
                _numberOfRepayment = value;
            }
        }
        public double Rate { get; internal set; }
        public double RepaymentAmount { get; internal set; }
        public double TotalRepayment
        {
            get
            {
                return RepaymentAmount * NumberOfRepayments;
            }
        }
    }
}
