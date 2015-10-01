using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramBuilder
{
    public class AnagramMatchTracker
    {
        private List<char> anagramRest;
        private readonly Stack<string> anagramWords;

        public AnagramMatchTracker(string anagram)
            : this(anagram, null)
        {
        }

        private AnagramMatchTracker(string anagram, Stack<string> words)
        {
            if (string.IsNullOrWhiteSpace(anagram))
                throw new ArgumentException("anagram");

            anagramRest = anagram.Replace(" ", "").ToList();
            anagramWords = words ?? new Stack<string>();
        }

        public bool Add(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            var anagramMatcherClone = new List<char>(anagramRest);
            var found = word.All(letter => anagramMatcherClone.Remove(letter));

            if (found)
            {
                anagramRest = anagramMatcherClone;
                anagramWords.Push(word);
            }

            return found;
        }

        public void Remove()
        {
            var word = anagramWords.Pop();
            foreach (var letter in word)
            {
                anagramRest.Add(letter);
            }
        }

        public string AnagramRest
        {
            get { return string.Join("", anagramRest); }
        }

        public int Count
        {
            get { return anagramWords.Count; }
        }

        public bool IsAnagramRestEmpty
        {
            get { return anagramRest.Count == 0; }
        }

        public override string ToString()
        {
            return string.Join(" ", anagramWords);
        }

        public AnagramMatch SaveAnagramMatch()
        {
            return new AnagramMatch(new List<string>(anagramWords));
        }
    }
}
