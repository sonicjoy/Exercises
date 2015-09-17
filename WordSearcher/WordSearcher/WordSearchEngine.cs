using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordSearcher
{
    public static class WordSearchEngine
    {
        public static void FindPath(string dictFilePath, string startWord, string endWord, string resultFilePath)
        {
            if (string.IsNullOrEmpty(dictFilePath) || string.IsNullOrEmpty(startWord) || string.IsNullOrEmpty(endWord) || string.IsNullOrEmpty(resultFilePath))
                throw new ArgumentException("Missing arguments");

            var wordSet = File.ReadAllLines(dictFilePath);
            var wordDict = new WordDictionary(wordSet);
            var resultSet = GetPathResult(wordDict, startWord, endWord);           
            File.WriteAllLines(resultFilePath, resultSet);
        }

        public static List<string> GetPathResult(WordDictionary wordDict, string startWord, string endWord)
        {         
            var resultSet = new List<string> { { "No results" } };
            if (wordDict.WordSet.ContainsKey(startWord))
            {
                resultSet.Clear();
                resultSet.Add(startWord);

                if (startWord == endWord || !wordDict.WordSet.ContainsKey(endWord)) return resultSet;

                var explored = new HashSet<string>(new string[] { startWord });
                var frontier = wordDict.WordSet[startWord].Except(explored).ToList();

                while (frontier.Any())
                {
                    var newStartWord = frontier.OrderBy(w => WordDictionary.GetWordDistance(w, endWord)).FirstOrDefault();
                    frontier.Remove(newStartWord);
                    resultSet.Add(newStartWord);
                    if (newStartWord == endWord)
                    {
                        break;
                    }
                    explored.Add(newStartWord);
                    var newNeighbors = wordDict.WordSet[newStartWord].Except(explored).Except(frontier).ToList();
                    frontier.AddRange(newNeighbors);
                }
                if (!resultSet.Contains(endWord))
                { 
                    resultSet.Clear();
                    resultSet.Add(startWord);
                }
            }
            return resultSet;
        }
    }
}