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
            var words = new NumberWords(number);
            return words.ConvertToString();
        }
    }

}
