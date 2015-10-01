using System;
using NUnit.Framework;

namespace AnagramBuilder.Tests
{
    [TestFixture]
    public class AnagramMatchTrackerTest
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Constructor_InvalidAnagramArgument_ArgumentException(string anagram)
        {
            Assert.Throws<ArgumentException>(() => new AnagramMatchTracker(anagram));
        }

        [TestCase]
        public void Constructor_ValidAnagramArgument_NewInstance()
        {
            var anagramMatchTracker = new AnagramMatchTracker("anagram");

            Assert.NotNull(anagramMatchTracker);
            Assert.AreEqual(0, anagramMatchTracker.Count);
            Assert.False(anagramMatchTracker.IsAnagramRestEmpty);
        }

        [TestCase("anagram", "anagram")]
        [TestCase("some fancy anagram", "somefancyanagram")]
        [TestCase("a b c d e f", "abcdef")]
        public void AnagramRest_NewInstanceWithValidAnagramArgument_AnagramRestWithWhitespaceRemoved(string anagram, string expectedAnagramRest)
        {
            var anagramMatchTracker = new AnagramMatchTracker(anagram);

            Assert.AreEqual(expectedAnagramRest, anagramMatchTracker.AnagramRest);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase("xyz")]
        public void Add_InvalidWordArgument_FalseAndUnchangedAnagramRest(string word)
        {
            var anagramMatchTracker = new AnagramMatchTracker("anagram");
            var existingAnagramRest = anagramMatchTracker.AnagramRest;
            Assert.False(anagramMatchTracker.Add(word));
            Assert.AreEqual(existingAnagramRest, anagramMatchTracker.AnagramRest);
        }

        [TestCase("a", "a", "")]
        [TestCase("anagram", "a", "nagram")]
        [TestCase("a b c d e f", "b", "acdef")]
        [TestCase("abc def hij", "bei", "acdfhj")]
        public void Add_ValidWordArgument_TrueAndChangeAnagramRest(string anagram, string word, string expectedAnagramRest)
        {
            var anagramMatchTracker = new AnagramMatchTracker(anagram);

            Assert.True(anagramMatchTracker.Add(word));
            Assert.AreEqual(expectedAnagramRest, anagramMatchTracker.AnagramRest);
        }

        [Test]
        public void Remove_FullAnagramRest_InvalidOperationException()
        {
            var anagramMatchTracker = new AnagramMatchTracker("anagram");

            Assert.Throws<InvalidOperationException>(() => anagramMatchTracker.Remove());
        }

        [TestCase("a", "a", "a")]
        [TestCase("anagram", "a", "nagrama")]
        [TestCase("a b c d e f", "b", "acdefb")]
        [TestCase("abc def hij", "bei", "acdfhjbei")]
        public void Remove_AddWordAndRemoveItAgain_AnagramRestSame(string anagram, string word, string expectedAnagramRest)
        {
            var anagramMatchTracker = new AnagramMatchTracker(anagram);
            anagramMatchTracker.Add(word);
            anagramMatchTracker.Remove();

            Assert.AreEqual(expectedAnagramRest, anagramMatchTracker.AnagramRest);
        }
    }
}
