using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringConverter
{
    internal class BritishEnglishNumber : INumberWords
    {
        const int SCALE_STEP = 3;
        private int _number = 0;
        private static readonly string[] scaleNames = { string.Empty, " thousand", " million", " billion", " trillion" };
        private static readonly string[] tensNames = { string.Empty, "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static readonly string[] numNames = {string.Empty, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                                                        "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        public BritishEnglishNumber(int number)
        {
            _number = number;
        }

        public string ConvertToString()
        {
            var words = new StringBuilder();
            if (_number == 0) words.Append("zero");
            else
            {
                if (_number < 0)
                {
                    words.Append("negative ");
                    _number = Math.Abs(_number);
                }
                var threeDigitSets = BuildThreeDigitSets();
                for (var i = threeDigitSets.Count - 1; i >= 0; i--)
                {
                    var threeDigitWords = threeDigitSets[i].ConvertToString();
                    if (!string.IsNullOrEmpty(threeDigitWords))
                    {
                        if (i < threeDigitSets.Count - 1)
                        {
                            if (i > 0)
                                words.Append(", ");
                            else if (i == 0)
                            {
                                if (threeDigitWords.Contains("hundred")) words.Append(", ");
                                else words.Append(" and ");
                            }
                        }
                        words.Append(threeDigitWords);
                        words.Append(scaleNames[i]);
                    }

                }
            }
            return words.ToString();
        }

        private SortedList<int, ThreeDigitSet> BuildThreeDigitSets()
        {
            var numberString = _number.ToString();
            var length = numberString.Length;
            var remainder = length % SCALE_STEP;
            if (remainder > 0)
            {
                var complementZeros = new string('0', SCALE_STEP - remainder);
                numberString = complementZeros + numberString;
                length = numberString.Length; //have to recalculate the length after adding dummy zeros
            }

            var numberOfSets = length / SCALE_STEP;
            var threeDigitSets = new SortedList<int, ThreeDigitSet>();
            for (var i = 0; i < numberOfSets; i++)
            {
                var defaultSet = new char[SCALE_STEP];
                for (var j = 0; j < SCALE_STEP; j++)
                {
                    var index = length -1 - i * SCALE_STEP - j;
                    defaultSet[j] = numberString[index];
                }
                var threeDigitSet = new ThreeDigitSet(defaultSet);
                threeDigitSets[i] = threeDigitSet;
            }

            return threeDigitSets;
        }

        private class ThreeDigitSet : IDigitSet
        {
            private string _unit = "0";
            private string _ten = "0";
            private string _hundred = "0";
            private StringBuilder _words = new StringBuilder();

            public ThreeDigitSet(char[] defaultSet)
            {
                _unit = defaultSet[0].ToString();
                _ten = defaultSet[1].ToString();
                _hundred = defaultSet[2].ToString();
            }

            public string ConvertToString()
            {
                BuildThreeDigitWords();
                return _words.ToString();
            }

            private void BuildThreeDigitWords()
            {
                if (_hundred != "0")
                {
                    AppendHundredsPart();
                    if (_ten != "0" || _unit != "0")
                        _words.Append(" and ");
                }

                AppendTensPart();
            }

            private void AppendHundredsPart()
            {
                var value = int.Parse(_hundred);
                if (value > 0)
                {
                    _words.Append(GetSpecialNumberName(value));
                    _words.Append(" ");
                    _words.Append("hundred");
                }
            }

            private void AppendTensPart()
            {
                var value = int.Parse(_ten + _unit);
                if (value > 0)
                {
                    if (value < 20)
                        _words.Append(GetSpecialNumberName(value));
                    else
                    {
                        var tensValue = int.Parse(_ten);
                        _words.Append(tensNames[tensValue]);
                        if (_unit != "0")
                            _words.Append(" ");
                        AppendUnitsPart();
                    }
                }
            }

            private void AppendUnitsPart()
            {
                var value = int.Parse(_unit);
                if (value > 0)
                    _words.Append(GetSpecialNumberName(value));
            }

            private string GetSpecialNumberName(int value)
            {
                var numName = string.Empty;
                if (value > 0 && value < 20) numName = numNames[value];
                return numName;
            }
        }
    }

}