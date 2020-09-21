using System;
using System.Collections.Generic;
using System.Linq;
using DictionaryAssistantMVC.Dictionary.Exceptions;

namespace DictionaryAssistantMVC.Dictionary
{
    public class DictionaryLetter
    {
        public int AverageCharacterCount { get; private set; }
        public int NumberWordsBeginningWith { get; private set; }
        public int NumberWordsEndingWith { get; private set; }
        public List<string> WordsBeginningWith { get; private set; }

        public static DictionaryLetter InitializeDictionaryLetter(char letter, IWordDictionary wordDictionary)
        {
            int beginWith, endWith, avgCount;
            List<string> words, wordsBeginWith;

            try
            {
                words = wordDictionary.GetDictionaryWordsStartingWith(letter);
                beginWith = words.Count;
                wordsBeginWith = words;
            }
            catch (WordStartingNotFoundException)
            {
                words = wordDictionary.GetDictionaryWords();
                words = words.FindAll(s => s[0] == letter);
                wordDictionary.SaveWordsForLetterStarting(letter, words);

                beginWith = words.Count;
                wordsBeginWith = words;
            }

            try
            {
                words = wordDictionary.GetDictionaryWordsEndingWith(letter);
                endWith = words.Count;
            }
            catch(WordEndingNotFoundException)
            {
                words = wordDictionary.GetDictionaryWords();
                words = words.FindAll(s => s.Last() == letter);
                wordDictionary.SaveWordsForLettersEnding(letter, words);

                endWith = words.Count;
            }

            if (wordsBeginWith.Count > 0)
            {
                // Get the average character length of all the words that start with `letter`.
                avgCount = wordsBeginWith.Aggregate(0, (acc, val) => acc + val.Length, avg => avg / wordsBeginWith.Count);
            }
            else
            {
                avgCount = 0;
            }

            return new DictionaryLetter(beginWith, endWith, avgCount, wordsBeginWith);
        }

        public List<string> GetLongestWords()
        {
            return WordsBeginningWith.Where(s => s.Length >= AverageCharacterCount).ToList();
        }

        public List<string> GetShortestWords()
        {
            return WordsBeginningWith.Where(s => s.Length < AverageCharacterCount).ToList();
        }

        protected DictionaryLetter(int beginWith, int endWith, int avgCount, List<string> wordsBeginingWith)
        {
            AverageCharacterCount = avgCount;
            NumberWordsBeginningWith = beginWith;
            NumberWordsEndingWith = endWith;
            WordsBeginningWith = wordsBeginingWith;
        }
    }
}
