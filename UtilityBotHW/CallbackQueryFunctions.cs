namespace UtilityBotHW
{
    internal class CallbackQueryFunctions
    {
        public int GetSumNumbers(string numbersString)
        {
            string[] numbers = numbersString.Split(' ');

            int sum = 0;
            foreach (string number in numbers)
            {
                if (int.TryParse(number, out int parsedNumber))
                {
                    sum += parsedNumber;
                }
            }
            return sum;
        }
    }
}
