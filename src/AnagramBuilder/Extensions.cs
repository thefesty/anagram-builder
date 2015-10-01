using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AnagramBuilder
{
    public static class Extensions
    {
        public static bool CanDeriveFromAnagram(this string word, string anagram)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            if (string.IsNullOrWhiteSpace(anagram))
                return false;

            var lettersInWord = word.ToList();
            var lettersInAnagram = anagram.ToList();

            return lettersInWord.All(letterInWord => lettersInAnagram.Remove(letterInWord));
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
        /// </summary>
        /// <param name="thisString"></param>
        /// <returns></returns>
        public static string GetMd5(this string thisString)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(thisString));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        public static string FormatTime(this long millisecounds)
        {
            TimeSpan t = TimeSpan.FromMilliseconds(millisecounds);
            return string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                            t.Hours,
                            t.Minutes,
                            t.Seconds,
                            t.Milliseconds);
        }
    }
}
