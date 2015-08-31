namespace NumberStringConverter
{
    interface INumberStringConverter
    {
        string ConvertNumberToWords(int number);
    }

    internal interface INumberWords
    {
        string ConvertToString();
    }

    internal interface IDigitSet
    {
        string ConvertToString();
    }
}
