using System;
using System.Collections.Generic;
using System.Linq;
using DictionaryAssistantMVC.Dictionary.Exceptions;

namespace DictionaryAssistantMVC.Dictionary
{
    public class ActiveDictionary : WordDictionaryBase
    {
        private SortedSet<DictionaryLetter> dictionaryLetters;

        public ActiveDictionary(List<string> allDictionaryWords)
            : base(allDictionaryWords)
        {
            dictionaryLetters = null;
        }

        public override ICollection<DictionaryLetter> GetAllDictionaryLetters()
        {
            if (dictionaryLetters == null)
            {
                CreateLetters();
            }

            return dictionaryLetters;
        }

        public override DictionaryLetter GetDictionaryLetter(char letter)
        {
            if (dictionaryLetters == null)
            {
                CreateLetters();
            }

            try
            {
                return dictionaryLetters.Where(dl => dl.Letter == letter).First();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        private void CreateLetters()
        {
            dictionaryLetters = new SortedSet<DictionaryLetter>(new DictionaryLetterComparer());

            foreach (char c in new Alphabet())
            {
                var letter = DictionaryLetter.InitializeDictionaryLetter(c, this);

                if (letter.NumberWordsBeginningWith > 0 || letter.NumberWordsEndingWith > 0)
                {
                    dictionaryLetters.Add(letter);
                }
            }
        }
    }
}
