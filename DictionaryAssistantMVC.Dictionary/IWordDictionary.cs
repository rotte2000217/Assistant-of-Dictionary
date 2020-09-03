using System;
using System.Collections.Generic;

namespace DictionaryAssistantMVC.Dictionary
{
    public interface IWordDictionary
    {
        List<string> GetDictionaryWords();

        List<string> GetDictionaryWordsStartingWith(char letter);
        List<string> GetDictionaryWordsEndingWith(char letter);

        void SaveWordsForLetterStarting(char letter, List<string> words);
        void SaveWordsForLettersEnding(char letter, List<string> words);
    }
}
