using System.Collections.Generic;

namespace AnagramBuilder
{
    public class AnagramMatch
    {
        private readonly List<string> anagramWords;

        public AnagramMatch(List<string> anagramWords)
        {
            this.anagramWords = anagramWords;
        }

        public override string ToString()
        {
            return string.Join(" ", anagramWords);
        }
    }
}