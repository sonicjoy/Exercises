using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordSearcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearcher.Tests
{
    [TestClass()]
    public class WordSearchEngineTests
    {
        private string[] stringArray = new string[] { "spin", "spit", "spat", "spot", "span" };
        WordDictionary wordDict;

        [TestInitialize]
        public void SetUpTest()
        {
            wordDict = new WordDictionary(stringArray);
        }

        [TestMethod]
        public void ReturnNoResultsIfStartWordNotInDict()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "xxxx", "xxxx");
            Assert.AreEqual("No results", result.FirstOrDefault());
        }

        [TestMethod]
        public void ReturnStartWordIfStartWordSameAsEndWord()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "spin", "spin");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("spin", result.FirstOrDefault());
        }

        [TestMethod]
        public void ReturnStartWordIfEndWordNotInDict()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "spin", "xxxx");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("spin", result.FirstOrDefault());
        }

        [TestMethod()]
        public void GetPathResultTest()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "spin", "spot");
            Assert.IsTrue(result.Contains("spin"));
            Assert.IsTrue(result.Contains("spit"));
            Assert.IsTrue(result.Contains("spot"));
            Assert.IsFalse(result.Contains("spat"));
            Assert.IsFalse(result.Contains("span"));
        }

        [TestMethod()]
        public void GetPathResultTestReverse()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "spot", "spin");
            Assert.IsTrue(result.Contains("spot"));
            Assert.IsTrue(result.Contains("spit"));
            Assert.IsTrue(result.Contains("spin"));
            Assert.IsFalse(result.Contains("span"));
            Assert.IsFalse(result.Contains("spat"));
        }

        [TestMethod()]
        public void GetPathResultTestLong()
        {
            var result = WordSearchEngine.GetPathResult(wordDict, "span", "spot");
            Assert.IsTrue(result.Contains("span"));
            Assert.IsTrue(result.Contains("spat"));
            Assert.IsTrue(result.Contains("spot"));
            Assert.IsFalse(result.Contains("spin"));
            Assert.IsFalse(result.Contains("spit"));
        }

        [TestMethod]
        public void GetPathResultTestSpecialCase()
        {
            var stringArraySpecial = new string[] { "abaa", "aaaa", "abza", "abzz", "aazz" };
            var wordDictSpecial = new WordDictionary(stringArraySpecial);

            var result = WordSearchEngine.GetPathResult(wordDictSpecial, "aaaa", "aazz");
            Assert.IsTrue(result.Contains("aaaa"));
            Assert.IsTrue(result.Contains("abaa"));
            Assert.IsTrue(result.Contains("abza"));
            Assert.IsTrue(result.Contains("abzz"));
            Assert.IsTrue(result.Contains("aazz"));
        }

        [TestMethod]
        public void GetPathResultTestSpecialCaseMultipleRoutes()
        {
            var stringArraySpecial = new string[] { "abaa", "aaaa", "abza", "abzz", "aazz", "azza" };
            var wordDictSpecial = new WordDictionary(stringArraySpecial);

            var result = WordSearchEngine.GetPathResult(wordDictSpecial, "aaaa", "aazz");
            Assert.IsTrue(result.Contains("aaaa"));
            Assert.IsTrue(result.Contains("abaa"));
            Assert.IsTrue(result.Contains("abza"));
            Assert.IsTrue(result.Contains("abzz"));
            Assert.IsTrue(result.Contains("aazz"));
            Assert.IsFalse(result.Contains("azzz"));
            Assert.IsFalse(result.Contains("azza"));
        }

        [TestMethod]
        public void GetPathResultTestSpecialCaseCircularRoutes()
        {
            var stringArraySpecial = new string[] { "abaa", "aaaa", "abza", "abzz", "aazz", "azza", "aaaz" };
            var wordDictSpecial = new WordDictionary(stringArraySpecial);

            var result = WordSearchEngine.GetPathResult(wordDictSpecial, "aaaa", "aazz");
            Assert.IsTrue(result.Contains("aaaa"));
            Assert.IsTrue(result.Contains("aaaz"));
            Assert.IsTrue(result.Contains("aazz"));
            Assert.IsFalse(result.Contains("abzz"));
            Assert.IsFalse(result.Contains("abaa"));
            Assert.IsFalse(result.Contains("abza"));
            Assert.IsFalse(result.Contains("azza"));
        }

        [TestMethod]
        public void GetPathResultTestSpecialCaseNoRoutes()
        {
            var stringArraySpecial = new string[] { "abaa", "aaaa", "abza", "abzz", "zzzz", "azza", "aaaz" };
            var wordDictSpecial = new WordDictionary(stringArraySpecial);

            var result = WordSearchEngine.GetPathResult(wordDictSpecial, "aaaa", "zzzz");
            Assert.IsTrue(result.Contains("aaaa"));
            Assert.IsFalse(result.Contains("aaaz"));
            Assert.IsFalse(result.Contains("zzzz"));
            Assert.IsFalse(result.Contains("abzz"));
            Assert.IsFalse(result.Contains("abaa"));
            Assert.IsFalse(result.Contains("abza"));
            Assert.IsFalse(result.Contains("azza"));
        }
    }
}