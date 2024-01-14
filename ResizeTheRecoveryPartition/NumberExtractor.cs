using System.Text.RegularExpressions;

namespace ResizeTheRecoveryPartition
{
    internal class NumberExtractor
    {
        public static int ExtractNumber(string input)
        {
            string resultString = Regex.Match(input, @"\d+").Value;
            int number = Int32.Parse(resultString);
            return number;
        }
    }
}
