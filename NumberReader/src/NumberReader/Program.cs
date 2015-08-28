using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberReader
{
    public class Program
    {
        public void Main(string[] args)
        {
            Console.WriteLine("Enter the number:");
            var input = Console.ReadLine();
            var output = ConvertNumberStringToEnglishWords(input);
            Console.WriteLine(output);
            Console.ReadLine();
        }

        /// <summary>
        /// Take a number string and return the equivalent number in British English words up to 999,999,999
        /// e.g. 1 = one
        /// 21 = twenty one
        /// 105 = one hundred and five
        /// 56945781 = fifty six million, nine hundred and forty five thousand, seven hundred and eighty one
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns>string of equivalent number in British English words</returns>
        public string ConvertNumberStringToEnglishWords(string numberString)
        {
            throw new NotImplementedException();
        }
    }
}
