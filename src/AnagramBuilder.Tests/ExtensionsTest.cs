using NUnit.Framework;

namespace AnagramBuilder.Tests
{
    [TestFixture]
    public class ExtensionsTest
    {
        [TestCase("", "")]
        [TestCase("   ", "")]
        [TestCase("", null)]
        [TestCase("", "   ")]
        [TestCase("a", "")]
        [TestCase("a", "b")]
        [TestCase("a b", "ab")]
        [TestCase("", "a")]
        [TestCase("a", "xyz")]
        [TestCase("abc", "xyz ab def")]
        public void CanDeriveFromAnagram_InvalidAnagramArgument_False(string word, string anagram)
        {
            Assert.False(word.CanDeriveFromAnagram(anagram));
        }
        
        [TestCase("a", "a")]
        [TestCase("a", "b a b")]
        [TestCase("ab", "cc a cc b")]
        [TestCase("a", "xyzaxyz")]
        [TestCase("abc", "xyaz debf hicj")]
        public void CanDeriveFromAnagram_ValidAnagramArgument_True(string word, string anagram)
        {
            Assert.True(word.CanDeriveFromAnagram(anagram));
        }
    }
}
