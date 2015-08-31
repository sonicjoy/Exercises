namespace NumberStringConverter
{
    public class NumberToBritishEnglishConverter : INumberStringConverter
    {
        public string ConvertNumberToWords(int number)
        {
            var words = new BritishEnglishNumber(number);
            return words.ConvertToString();
        }
    }

}
