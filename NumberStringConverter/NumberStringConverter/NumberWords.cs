using System;
using System.Collections.Generic;
using System.Text;

namespace NumberStringConverter
{
    internal class NumberWords
    {
        const int SCALE_STEP = 3;
        private int _number = 0;
        private StringBuilder _words = new StringBuilder();
        private static readonly string[] scaleNames = { string.Empty, " thousand", " million", " billion", " trillion" };

        public NumberWords(int number)
        {
            if (number < 0)
            {
                _words.Append("negative");
                _number = Math.Abs(number);
            }
            else
                _number = number;

        }

        public string ConvertToString()
        {
            if (_number == 0) _words.Append("zero");
            else
            {
                var threeDigitSets = BuildThreeDigitSets();
                for (var i = threeDigitSets.Count - 1; i >= 0; i--)
                {
                    var threeDigitWords = threeDigitSets[i].ConvertToString();
                    if (!string.IsNullOrEmpty(threeDigitWords))
                    {
                        _words.Append(threeDigitWords);//append the 3 digit words
                        _words.Append(scaleNames[i]); //append the scale name
                    }
                    
                }
            }
            return _words.ToString();
        }

        private SortedList<int, ThreeDigitSet> BuildThreeDigitSets()
        {
            var threeDigitSets = new SortedList<int, ThreeDigitSet>();
            var numberString = _number.ToString();
            var length = numberString.Length;
            var numberOfThree = length / SCALE_STEP;
            var remainder = length % SCALE_STEP;

            for (var i = 0; i < numberOfThree; i++)
            {
                var unit = numberString[length - 1 - i * SCALE_STEP];
                var ten = numberString[length - 2 - i * SCALE_STEP];
                var hundred = numberString[length - 3 - i * SCALE_STEP];
                var threeDigitSet = new ThreeDigitSet(hundred, ten, unit);
                threeDigitSets[i] = threeDigitSet;
            }
            if (remainder == 2)
            {
                var ten = numberString[0];
                var unit = numberString[1];
                var threeDigitSet = new ThreeDigitSet(ten, unit);
                threeDigitSets[numberOfThree] = threeDigitSet;
            }
            else if (remainder == 1)
            {
                var unit = numberString[0];
                var threeDigitSet = new ThreeDigitSet(unit);
                threeDigitSets[numberOfThree] = threeDigitSet;
            }

            return threeDigitSets;
        }

        private class ThreeDigitSet
        {
            private string _unit = "0";
            private string _ten = "0";
            private string _hundred = "0";
            private StringBuilder _words = new StringBuilder();

            public ThreeDigitSet(char hundred, char ten, char unit) : this(ten, unit)
            {
                _hundred = hundred.ToString();
            }

            public ThreeDigitSet(char ten, char unit) : this(unit)
            {
                _ten = ten.ToString();
            }

            public ThreeDigitSet(char unit)
            {
                _unit = unit.ToString();
            }

            internal string ConvertToString()
            {
                BuildWords();
                return _words.ToString();
            }

            private void BuildWords()
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
                _words.Append(GetSpecialNumberName(value));
                _words.Append(" ");
                _words.Append("hundred");
            }

            private void AppendTensPart()
            {
                var tensNames = new string[] { string.Empty, "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                var value = int.Parse(_ten + _unit);
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

            private void AppendUnitsPart()
            {
                var value = int.Parse(_unit);
                _words.Append(GetSpecialNumberName(value));
            }

            private string GetSpecialNumberName(int value)
            {
                var numNames = new string[]{string.Empty, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                                                        "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var numName = string.Empty;
                if (value > 0 && value < 20) numName = numNames[value];
                return numName;
            }
        }
    }
}