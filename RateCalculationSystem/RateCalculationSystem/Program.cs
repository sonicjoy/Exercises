using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCalculationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Your requested loan will be rounded to the nearest 100.");
                var controller = new Controller(args);
                var formattedQuote = controller.GetQuote();
                Console.WriteLine(formattedQuote);
                Environment.Exit(0);
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(1);
            }
        }
    }
}
