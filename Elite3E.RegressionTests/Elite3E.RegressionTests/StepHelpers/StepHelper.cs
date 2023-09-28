using System;

namespace Elite3E.RegressionTests.StepHelpers
{
    public static class StepHelper
    {
        public static string GetRandomString(int minLen, int maxLen, bool addAutomation = false)
        {
            char[] alphabet = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcefghijklmnopqrstuvwxyz0123456789").ToCharArray();
            Random randomInstance = new Random();
            var randLock = new object();

            int alphabetLength = alphabet.Length;
            int stringLength;

            lock (randLock)
            {
                stringLength = randomInstance.Next(minLen, maxLen);
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

            return addAutomation ? "automation " + new string(str) : new string(str);

        }
    }
}
