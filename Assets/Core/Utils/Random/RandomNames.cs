using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Core
{
    public static class RandomUtils
    {
        public static T RandomItem<T>(T[] items)
        {
            int index = UnityEngine.Random.Range(0, items.Length);

            return items[index];
        }
        public static T RandomItem<T>(IEnumerable<T> items)
        {
            return RandomItem(items.ToArray());
        }

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            //System.Random random = new System.Random();

            char ch;
            for (int i = 0; i < size; i++)
            {
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                ch = Convert.ToChar(Convert.ToInt32(65 + UnityEngine.Random.Range(0, 26)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }

        public static string GenerateInvestmentCompanyName()
        {
            string[] names = new string[] { "Investments", "Capitals", "Funds", "Wealth Management", "and partners" };

            int length = UnityEngine.Random.Range(4, 8);

            return "The " + RandomString(length, true) + " " + RandomItem(names);
        }
    }
}
