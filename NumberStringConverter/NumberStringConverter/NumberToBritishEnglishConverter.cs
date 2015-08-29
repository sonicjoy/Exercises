using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberStringConverter
{
    public class NumberToBritishEnglishConverter : INumberStringConverter
    {
        private static readonly string[] scaleNames = { string.Empty, "thousand", "million", "billion"};
        private static readonly string[] tensNames = { string.Empty, "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] numNames = { string.Empty, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                                                        "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        public string ConvertNumberToWords(int number)
        {
            var words = new StringBuilder();
            if (number == 0) words.Append("zero");
            if (number < 0)
            {
                words.Append("negative");
                number = Math.Abs(number);
            }

            else
            {
                var numberString = number.ToString();
                var length = numberString.Length;
                var numberOfThree = length / 3;
                var remainder = length % 3;

            }

            return words.ToString();
        }

        private string BuildThreeDigitSet(int number)
        {
            throw new NotImplementedException();
        }

        private string BuildHundredPart(int number)
        {
            throw new NotImplementedException();
        }

        private string BuildTenPart(int number)
        {
            throw new NotImplementedException();
        }

        private string BuildUnitPart(int number)
        {
            var unitName = string.Empty;
            if (number > 0 && number < 19)
                unitName = numNames[number];
            return unitName;
        }

    }

}
