using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictionaryAssistantMVC.Dictionary;
using DictionaryAssistantMVC.Dictionary.Exceptions;

namespace DictionaryAssistantMVC.Dictionary.Tests
{
    public class MockDictionary : IWordDictionary
    {
        private static readonly string[] Words = new string[]
        {
            "abbreviations",
            "abdicates",
            "abductions",
            "abductors",
            "abilities",
            "stammer",
            "stamp",
            "stance",
            "standard",
            "standstill"
        };

        private readonly string[] mockWords;

        public MockDictionary()
        {
            mockWords = MockDictionary.Words;
        }

        public MockDictionary(string[] wordsToUse)
        {
            mockWords = wordsToUse;
        }

        public List<string> GetDictionaryWords()
        {
            return new List<string>(mockWords);
        }

        public List<string> GetDictionaryWordsEndingWith(char letter)
        {
            throw new WordEndingNotFoundException();
        }

        public List<string> GetDictionaryWordsStartingWith(char letter)
        {
            throw new WordStartingNotFoundException();
        }

        public void SaveWordsForLettersEnding(char letter, List<string> words)
        {
            return;
        }

        public void SaveWordsForLetterStarting(char letter, List<string> words)
        {
            return;
        }
    }
}
