using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberStringConverter
{
    public class NumberToBritishEnglishConverter : INumberStringConverter
    {
        public string ConvertNumberToWords(int number)
        {
            var words = new StringBuilder();
            if (number == 0) words.Append("zero");
            else
            {
                var numberString = number.ToString();
                var length = numberString.Length;

            }

            return words.ToString();
        }

        private string BuildThreeDigitSet(string numberString)
        {
            throw new NotImplementedException();
        }

        private string BuildHundredPart(string numberString)
        {
            throw new NotImplementedException();
        }

        private string BuildTenPart(string numberString)
        {
            throw new NotImplementedException();
        }

        private string BuildUnitPart(string numberString)
        {
            throw new NotImplementedException();
        }

    }
}
