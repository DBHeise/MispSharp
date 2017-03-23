using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misp.Tests
{
    internal static class TestHelper
    {
        internal static String MispInstance = "http://misp.h2net.heiseink.com";
        internal static String MispKey = "S36SAVjXcDh8WeFMJDPLwHUjMx3dJDdWHPvXGXPh";

        private static Random random = new Random((int)DateTime.Now.Ticks);

        internal static String RandomString()
        {
            return RandomString(RandomInt(3, 30));
        }
        internal static String RandomSentance()
        {
            int numWords = RandomInt(5, 20);
            List<String> words = new List<String>();
            for (int i = 0; i < numWords; i++)
            {
                words.Add(RandomString(RandomInt(3, 10)));
            }

            return String.Join(" ", words) + ".";
        }
        internal static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }
        internal static Boolean RandomBool()
        {
            return (random.Next() % 2 == 0);
        }
        internal static int RandomInt(int min, int max)
        {
            return random.Next(min, max);
        }
        internal static T RandomFromList<T>(IList<T> list)
        {
            int max = list.Count;
            int idx = random.Next(0, max);
            return list[idx];
        }
        internal static String RandomHexString(int size)
        {
            Char[] charset = "0123456789ABCDEF".ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(RandomFromList(charset));
            }
            return sb.ToString();
        }


        public static Boolean Contains<T>(IList<T> list, T item, Func<T, T, Boolean> comparitor)
        {
            Boolean ans = false;
            for (int i = 0; i < list.Count && !ans; i++)
            {
                ans = comparitor(list[i], item);
            }
            return ans;
        }

        public static String GetHash(String file, String algo = "sha256")
        {
            byte[] hashBytes;
            System.IO.FileInfo nfo = new System.IO.FileInfo(file);
            using (System.Security.Cryptography.HashAlgorithm hasher = System.Security.Cryptography.HashAlgorithm.Create(algo))
            {
                using (System.IO.FileStream stream = nfo.OpenRead())
                {
                    hashBytes = hasher.ComputeHash(stream);
                }
            }
            return System.BitConverter.ToString(hashBytes).Replace("-", String.Empty);
        }
    }
}
