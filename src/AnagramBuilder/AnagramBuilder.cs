using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AnagramBuilder
{
    public class AnagramBuilder
    {
        private readonly string anagram;
        private readonly int maxAnagramWords;
        private readonly ICollection<string> anagramDictionary;
        private readonly ICollection<AnagramMatch> anagramCombinations = new List<AnagramMatch>();
        private long executionTime;

        public AnagramBuilder(string anagram, ICollection<string> anagramDictionary)
        {
            if (string.IsNullOrWhiteSpace(anagram))
                throw new ArgumentException("anagram");

            if (anagramDictionary == null)
                throw new ArgumentNullException("anagramDictionary");

            this.anagram = anagram;
            this.anagramDictionary = anagramDictionary;

            maxAnagramWords = anagram.Split(char.Parse(" ")).Length;
        }

        public long ExecutionTime
        {
            get { return executionTime; }
        }

        public ICollection<AnagramMatch> Build()
        {
            var watch = Stopwatch.StartNew();
            Build(anagramDictionary, new AnagramMatchTracker(anagram));
            watch.Stop();
            executionTime = watch.ElapsedMilliseconds;
            return anagramCombinations;
        }

        private void Build(ICollection<string> possibleWords, AnagramMatchTracker currentAnagramMatchTracker)
        {
            var matchingWords = GetWordsWhichMatchesAnagram(possibleWords, currentAnagramMatchTracker.AnagramRest);
            foreach (var word in matchingWords)
            {
                var canAdd = currentAnagramMatchTracker.Add(word);

                if (canAdd)
                {
                    if (AnagramMatchFound(currentAnagramMatchTracker))
                    {
                        anagramCombinations.Add(currentAnagramMatchTracker.SaveAnagramMatch());
                        currentAnagramMatchTracker.Remove();
                        continue;
                    }

                    if (CanAddMoreWords(currentAnagramMatchTracker))
                        Build(matchingWords, currentAnagramMatchTracker);

                    currentAnagramMatchTracker.Remove();
                }
            }
        }

        private ICollection<string> GetWordsWhichMatchesAnagram(ICollection<string> words, string anagram)
        {
            return words.Where(word => word.CanDeriveFromAnagram(anagram)).ToList();
        }

        private bool AnagramMatchFound(AnagramMatchTracker currentAnagramMatchTracker)
        {
            return currentAnagramMatchTracker.IsAnagramRestEmpty && currentAnagramMatchTracker.Count == maxAnagramWords;
        }

        private bool CanAddMoreWords(AnagramMatchTracker currentAnagramMatchTracker)
        {
            return !currentAnagramMatchTracker.IsAnagramRestEmpty && currentAnagramMatchTracker.Count < maxAnagramWords;
        }
    }
}
