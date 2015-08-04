using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculationSystem
{
    public class Controller
    {
        string _fileName;
        int _requestedAmount;
        MarketModel _marketData;

        public int NumberOfRepayment = 36;

        public Controller(string[] args)
        {
            try
            {
                _fileName = args[0] as string;
                _requestedAmount = Int32.Parse(args[1]);
                _marketData = new MarketModel(_fileName, true);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public string GetQuote()
        {
            var formattedQuote = string.Empty;
            Quote quote = RateCalculator.GetQuote(_marketData.LenderPool, _requestedAmount, NumberOfRepayment);
            formattedQuote = BuildFormattedQuote(quote);
            return formattedQuote;
        }

        private string BuildFormattedQuote(Quote quote)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Requested amount: ").AppendLine(quote.LoanAmount.ToString("C"));
            stringBuilder.Append("Rate: ").AppendLine(quote.Rate.ToString("P1"));
            stringBuilder.Append("Monthly repayment: ").AppendLine(quote.RepaymentAmount.ToString("C"));
            stringBuilder.Append("Total repayment: ").AppendLine(quote.TotalRepayment.ToString("C"));
            return stringBuilder.ToString();
        }
    }
}
