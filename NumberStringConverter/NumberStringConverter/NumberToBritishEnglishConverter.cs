namespace NumberStringConverter
{
    public class NumberToBritishEnglishConverter : INumberStringConverter
    {
        public string ConvertNumberToWords(int number)
        {
            var words = new NumberWords(number);
            return words.ConvertToString();
        }
    }

}
