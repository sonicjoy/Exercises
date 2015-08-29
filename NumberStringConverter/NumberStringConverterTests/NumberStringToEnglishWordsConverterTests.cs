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
        private readonly string[] numNames = {string.Empty, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                                                        "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private readonly string[] tensNames = { string.Empty, "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        [TestInitialize]
        public void InitialiseTheConverter()
        {
            converter = new NumberToBritishEnglishConverter();
        }

        [TestMethod]
        public void If_Value_Is_0_Convert_To_Zero()
        {
            var result = converter.ConvertNumberToWords(0);
            Assert.AreEqual("zero", result);
        }

        [TestMethod]
        public void If_Value_Is_1_To_19_Return_Single_Word()
        {
            for (var i = 1; i < 20; i++)
            {
                var result = converter.ConvertNumberToWords(i);
                Assert.AreEqual(numNames[i], result);
            }
        }

        [TestMethod]
        public void If_Value_Is_Tens_Return_The_ty_Word()
        {
            for (var i = 2; i <= 9; i = i + 10)
            {
                var result = converter.ConvertNumberToWords(i * 10);
                Assert.AreEqual(tensNames[i], result);
            }
        }

        [TestMethod]
        public void Two_Digit_Number_Greater_Than_20_With_Non_Zero_Unit_Test()
        {
            var result1 = converter.ConvertNumberToWords(21);
            Assert.AreEqual("twenty one", result1);

            var result2 = converter.ConvertNumberToWords(99);
            Assert.AreEqual("ninety nine", result2);
        }

        [TestMethod]
        public void Hundred_Value_Test()
        {
            var result = converter.ConvertNumberToWords(100);
            Assert.AreEqual("one hundred", result);
        }

        [TestMethod]
        public void Three_Digit_Value_Combination_Test()
        {
            var result1 = converter.ConvertNumberToWords(101);
            Assert.AreEqual("one hundred and one", result1);

            var result2 = converter.ConvertNumberToWords(111);
            Assert.AreEqual("one hundred and eleven", result2);

            var result3 = converter.ConvertNumberToWords(121);
            Assert.AreEqual("one hundred and twenty one", result3);
        }

        [TestMethod]
        public void Large_Than_3_Digits_Number_Scale_Test()
        {
            var result1 = converter.ConvertNumberToWords(1000);
            Assert.AreEqual("one thousand", result1);

            var result2 = converter.ConvertNumberToWords(1000000);
            Assert.AreEqual("one million", result2);

            var result3 = converter.ConvertNumberToWords(1000000000);
            Assert.AreEqual("one billion", result3);
        }

        [TestMethod]
        public void Large_Than_3_Digits_Number_Recombination_Test()
        {
            var result1 = converter.ConvertNumberToWords(56945781);
            Assert.AreEqual("fifty six million, nine hundred and forty five thousand, seven hundred and eighty one", result1);

            var result2 = converter.ConvertNumberToWords(1001100);
            Assert.AreEqual("one million, one thousand, one hundred", result2);

            var result3 = converter.ConvertNumberToWords(1000000012);
            Assert.AreEqual("one billion and twelve", result3);

            var result4 = converter.ConvertNumberToWords(1000100012);
            Assert.AreEqual("one billion, ten million and twelve", result4);
        }
    }
}