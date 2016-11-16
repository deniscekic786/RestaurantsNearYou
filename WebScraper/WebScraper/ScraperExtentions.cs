using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public static class ScraperExtentions
    {
        private static Random rng = new Random();
        /// <summary>
        /// Randomizes a list in order to use a method
        /// to retrieve a unique record
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        /// <summary>
        /// Splits the yelp address text and fetches the
        /// postal code by checking if the characters 
        /// on the side of the string or integers 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string ExtractPostalCode(string address)
        {
            StringBuilder zipCodeBuilder = new StringBuilder();
            var splitHalf = address.Split(',');
            var addressHalf = splitHalf[1];
            for (int i = 0; i < addressHalf.Length; i++)
            {
                if (char.IsDigit(addressHalf[i]))
                {
                    zipCodeBuilder.Append(addressHalf[i]);
                }
            }
            return zipCodeBuilder.ToString();
        }

    }
}
