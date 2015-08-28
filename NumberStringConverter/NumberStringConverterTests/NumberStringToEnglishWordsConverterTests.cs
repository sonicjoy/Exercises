using Microsoft.VisualStudio.TestTools.UnitTesting;
using NumberStringConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberStringConverter.Tests
{
    [TestClass]
    public class NumberStringToBritishEnglishWordsConverterTests
    {
        NumberToBritishEnglishConverter converter;

        [TestInitialize]
        public void InitialiseTheConverter()
        {
            converter = new NumberToBritishEnglishConverter();
        }

        [TestMethod]
        public void If_Value_Is_Zero_Convert_To_Zero()
        {
            var result = converter.ConvertNumberToWords(0);
            Assert.AreEqual("zero", result);
        }

        [TestMethod]
        public void If_Value_Is_Single_Digit_Convert_To_The_Number_Name()
        {
            var expectedResult = new String[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            for (var i = 1; i < 10; i++)
            {
                var result = converter.ConvertNumberToWords(i);
                Assert.AreEqual(expectedResult[i-1], result);
            }
        }
    }
}