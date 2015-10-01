using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AnagramBuilder.Tests
{
    [TestFixture]
    public class AnagramBuilderTest
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Constructor_InvalidAnagramArgument_ArgumentException(string anagram)
        {
            Assert.Throws<ArgumentException>(() => new AnagramBuilder(anagram, new List<string>()));
        }

        [TestCase]
        public void Constructor_InvalidAnagramDictionaryArgument_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new AnagramBuilder("anagram", null));
        }

        [TestCase]
        public void Build_AnagramWithOneWordAndValidAnagramDictionary_TwoAnagramMatches()
        {
            var anagramDictionary = new List<string> { "acb", "bac", "xyz", "yzx" };
            var anagramBuilder = new AnagramBuilder("abc", anagramDictionary);
            var expectedAnagramMatches = new List<string> { "acb", "bac" };

            var actualAnagramMatches = anagramBuilder.Build();
            
            Assert.AreEqual(2, actualAnagramMatches.Count);
            CollectionAssert.AreEquivalent(expectedAnagramMatches, actualAnagramMatches.Select(anagram => anagram.ToString()));
        }

        [TestCase]
        public void Build_AnagramWitThreeWordsAndValidAnagramDictionary_SixAnagramMatches()
        {
            var anagramDictionary = new List<string> { "a", "b", "c", "d", "e", "f" };
            var anagramBuilder = new AnagramBuilder("a b c", anagramDictionary);
            var expectedAnagramMatches = new List<string> { "a b c", "a c b", "b a c", "b c a", "c a b", "c b a" };

            var actualAnagramMatches = anagramBuilder.Build();

            Assert.AreEqual(6, actualAnagramMatches.Count);
            CollectionAssert.AreEquivalent(expectedAnagramMatches, actualAnagramMatches.Select(anagram => anagram.ToString()));
        }

        [TestCase]
        public void Build_AnagramContainsMoreLettersThanAvailableInAnagramDictionary_ZeroAnagramMatches()
        {
            var anagramDictionary = new List<string> { "a", "b", "c" };
            var anagramBuilder = new AnagramBuilder("aaa bbb ccc", anagramDictionary);

            var actualAnagramMatches = anagramBuilder.Build();

            CollectionAssert.IsEmpty(actualAnagramMatches);
        }
    }
}
