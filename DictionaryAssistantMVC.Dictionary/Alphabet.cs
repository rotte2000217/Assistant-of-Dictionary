using System;
using System.Collections;
using System.Collections.Generic;


namespace DictionaryAssistantMVC.Dictionary
{
    public class Alphabet : IEnumerable<char>
    {
        public const int AlphabetCount = 26;

        private readonly string alphabetLetters = "abcdefghijklmnopqrstuvwxyz";
        private class AlphabetEnumerator : IEnumerator<char>
        {
            private int currentIndex = -1;
            private readonly Alphabet theAlphabet;

            public AlphabetEnumerator(Alphabet abc)
            {
                theAlphabet = abc;
            }

            public char Current => theAlphabet.alphabetLetters[currentIndex];
            object IEnumerator.Current => Current;

            public void Dispose()
            {
                return;
            }

            public bool MoveNext()
            {
                return ++currentIndex < theAlphabet.alphabetLetters.Length;
            }

            public void Reset()
            {
                currentIndex = -1;
            }
        }

        public IEnumerator<char> GetEnumerator()
        {
            return new AlphabetEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
