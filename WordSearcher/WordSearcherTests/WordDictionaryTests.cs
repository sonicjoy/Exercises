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
    public class WordDictionaryTests
    {
        private string[] stringArray = new string[] { "spin", "spit", "spat", "spot", "span" };
        private WordDictionary dict;

        [TestInitialize]
        public void SetUpTest()
        {
            dict = new WordDictionary(stringArray);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowArgumentExceptionIfWordsHaveDifferentLength()
        {
            WordDictionary.GetWordDistance("asdf", "asdfg");
        }

        [TestMethod()]
        public void GetWordDistanceTest()
        {
            var dist1 = WordDictionary.GetWordDistance("abcd", "abce");
            Assert.AreEqual(1, dist1);

            var dist2 = WordDictionary.GetWordDistance("abcd", "abee");
            Assert.AreEqual(2, dist2);

            var dist3 = WordDictionary.GetWordDistance("abcd", "bcda");
            Assert.AreEqual(4, dist3);
        }

        [TestMethod()]
        public void WordDictionaryTest()
        {
            Assert.IsInstanceOfType(dict.WordSet, typeof(Dictionary<string, List<string>>));
            Assert.AreEqual(5, dict.WordSet.Keys.Count);
            Assert.IsTrue(dict.WordSet["spin"].Contains("spit"));
            Assert.IsTrue(dict.WordSet["spin"].Contains("span"));
            Assert.IsFalse(dict.WordSet["spin"].Contains("spat"));
            Assert.IsFalse(dict.WordSet["spin"].Contains("spot"));
        }

    }
}