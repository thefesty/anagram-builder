using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string anagram = "poultry outwits ants";
            const string md5Checksum = "4624d200580677270a54ccff86b9610e";

            var anagramDictionary = GetWordlist();

            Console.WriteLine("Searching...");

            var anagramBuilder = new AnagramBuilder(anagram, anagramDictionary);
            var anagramMatches = anagramBuilder.Build();

            var anagramMd5Match = FindAnagramWhichMatchesMd5Checksum(anagramMatches, md5Checksum);

            Console.WriteLine("Search done");
            Console.WriteLine("Search time: " + anagramBuilder.ExecutionTime.FormatTime());
            Console.WriteLine("Possible anagrams found: " + anagramMatches.Count);
            Console.WriteLine("Result: " + anagramMd5Match);
            Console.WriteLine("Finished");

            Console.ReadKey();
        }

        private static ICollection<string> GetWordlist()
        {
            return File.ReadAllLines(@"wordlist.txt").Distinct().ToList();
        }

        private static string FindAnagramWhichMatchesMd5Checksum(IEnumerable<AnagramMatch> anagramMatches, string md5Checksum)
        {
            var anagramMatch = anagramMatches.FirstOrDefault(anagram => anagram.ToString().GetMd5() == md5Checksum);
            return anagramMatch == null ? null : anagramMatch.ToString();
        }
    }
}

