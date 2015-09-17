using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearcher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("WordSearcher dictFile startWord endWord resultFile");
                var dictFile = args[0];
                var startWord = args[1];
                var endWord = args[2];
                var resultFile = args[3];
                //The required procedure call will output result into a file
                WordSearchEngine.FindPath(dictFile, startWord, endWord, resultFile);

                Console.ReadLine();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                Environment.Exit(1);
            }
        }
    }
}
