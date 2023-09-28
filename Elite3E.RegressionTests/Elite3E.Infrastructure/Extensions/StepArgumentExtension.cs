using System;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Elite3E.Infrastructure.Extensions
{
    public static class StepArgumentExtension
    {
        private const string Today = "{Today}";
        private const string CurrentYear = "{CurrentYear}";
        private const string Auto = "{Auto";

        public static void ReplaceTableDynamicValues(this Table dataToBeTransformed, string format)
        {
            foreach (TableRow row in dataToBeTransformed.Rows)
            {
                foreach (string key in row.Keys)
                {
                    string value = row[key];

                    value = GetReplaceValue(format, value);

                    row[key] = value;
                }
            }
        }

        private static string GetReplaceValue(string format, string textToReplace)
        {
            string match = Regex.Match(textToReplace, @"\{.*\}").ToString();

            if (string.IsNullOrEmpty(match)) return textToReplace;
            switch (match)
            {
                case Today:
                    {
                        textToReplace = ConvertExpressionToDate(textToReplace, format);
                        break;
                    }
                case CurrentYear:
                    {
                        textToReplace = ReplaceYearInTheExpression(textToReplace);
                        break;
                    }
                case string x when x.StartsWith(Auto):
                    {
                        textToReplace = GenerateRandomString(textToReplace);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("transformation not found for: {0}", textToReplace);
                        break;
                    }
            }

            return textToReplace;
        }

        public static void ReplaceDynamicValues(this Table dataToBeTransformed)
        {
            ReplaceTableDynamicValues(dataToBeTransformed, @"M/d/yyyy");
        }

        public static string ReplaceDynamicValues(string dataToBeTransformed, string format = @"dd/MM/yyyy")
        {
            return GetReplaceValue(format, dataToBeTransformed);
        }

        private static string ConvertExpressionToDate(string expression, string format)
        {
            var value = DateTime.Today;
            expression = expression.Replace(Today, string.Empty);

            if (!string.IsNullOrWhiteSpace(expression))
            {
                int multiplier = expression.Contains("+") ? 1 : -1;

                expression = expression.Replace("+", string.Empty).Replace("-", string.Empty).Trim();

                int days;
                bool success = int.TryParse(expression, out days);

                if (success)
                {
                    value = value.AddDays(days * multiplier);
                }
                else
                {
                    throw new Exception("Data cannot be converted to date " + expression);
                }
            }

            return value.ToString(format);
        }

        private static string ReplaceYearInTheExpression(string expression)
        {
            var value = DateTime.Today.Year;

            var modifiedExpression = expression.Replace(CurrentYear, value.ToString());

            return modifiedExpression;
        }

        private static string GenerateRandomString(string expression)
        {

            var length = Convert.ToInt32(expression.Replace(Auto, "").Replace("}", ""));

            if (length == 36) return "auto_" + Guid.NewGuid();

            char[] alphabet = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcefghijklmnopqrstuvwxyz0123456789").ToCharArray();
            Random randomInstance = new Random();
            var randLock = new object();

            int alphabetLength = alphabet.Length;
            int stringLength;

            lock (randLock)
            {
                stringLength = randomInstance.Next(length - 3, length - 3);
            }

            char[] str = new char[stringLength];

            // max length of the randomizer array is 5
            int randomizerLength = (stringLength > 5) ? 5 : stringLength;

            int[] rndInts = new int[randomizerLength];
            int[] rndIncrements = new int[randomizerLength];

            // Prepare a "randomizing" array
            for (int i = 0; i < randomizerLength; i++)
            {
                int rnd = randomInstance.Next(alphabetLength);
                rndInts[i] = rnd;
                rndIncrements[i] = rnd;
            }

            // Generate "random" string out of the alphabet used
            for (int i = 0; i < stringLength; i++)
            {
                int indexRnd = i % randomizerLength;
                int indexAlphabet = rndInts[indexRnd] % alphabetLength;
                str[i] = alphabet[indexAlphabet];

                rndInts[indexRnd] += rndIncrements[indexRnd];
            }

            return "at_" + new string(str);

        }

    }
}
