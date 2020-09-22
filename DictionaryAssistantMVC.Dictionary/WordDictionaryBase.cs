using System;
using System.Collections.Generic;
using System.Text;
using DictionaryAssistantMVC.Dictionary.Exceptions;


namespace DictionaryAssistantMVC.Dictionary
{
    public abstract class WordDictionaryBase : IWordDictionary
    {
        private readonly List<string> allDictionaryWords;
        private readonly Dictionary<char, List<String>> wordsEndingWith;
        private readonly Dictionary<char, List<String>> wordsStartingWith;

        public WordDictionaryBase(List<string> allDictionaryWords)
        {
            this.allDictionaryWords = allDictionaryWords;
            wordsEndingWith = new Dictionary<char, List<string>>();
            wordsStartingWith = new Dictionary<char, List<string>>();
        }

        /* Abstract Methods */

        public abstract DictionaryLetter GetDictionaryLetter(char letter);

        public abstract ICollection<DictionaryLetter> GetAllDictionaryLetters();

        /* Implements IWordDictionary: */

        public virtual List<string> GetDictionaryWords()
        {
            return allDictionaryWords;
        }

        public virtual List<string> GetDictionaryWordsEndingWith(char letter)
        {
            if (!wordsEndingWith.ContainsKey(letter))
            {
                throw new WordEndingNotFoundException();
            }

            return wordsEndingWith[letter];
        }

        public virtual List<string> GetDictionaryWordsStartingWith(char letter)
        {
            if (!wordsStartingWith.ContainsKey(letter))
            {
                throw new WordStartingNotFoundException();
            }

            return wordsStartingWith[letter];
        }

        public virtual void SaveWordsForLettersEnding(char letter, List<string> words)
        {
            wordsEndingWith[letter] = words;
        }

        public virtual void SaveWordsForLetterStarting(char letter, List<string> words)
        {
            wordsStartingWith[letter] = words;
        }
    }
}
