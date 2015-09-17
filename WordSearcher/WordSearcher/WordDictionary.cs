using System;
using System.Collections.Generic;
using System.Linq;

namespace WordSearcher
{
    public class WordDictionary
    {
        public readonly Dictionary<string, List<string>> WordSet;

        public WordDictionary(string[] stringArray)
        {
            WordSet = new Dictionary<string, List<string>>();
            foreach (var thisString in stringArray)
                WordSet[thisString] = new List<string>(stringArray.Where(otherString => GetWordDistance(thisString, otherString) == 1));
        }

        /// <summary>
        /// Calculate the total number of different characters, regardless whether the words are in the dictionary.
        /// e.g. the distance between "abce" and "abcd" is 1
        /// distance between "abcd" and "aeee" is 3
        /// distance between "abcd" and "bcda" is 4 
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns>integer value</returns>
        public static int GetWordDistance(string word1, string word2)
        {
            if (word1.Length != word2.Length)
                throw new ArgumentException("Two words have different length.");
            var distance = 0;
            for(var i = 0; i < word1.Length; i++)
                if (word1[i] != word2[i]) distance++;
            return distance;
        }
    }
}