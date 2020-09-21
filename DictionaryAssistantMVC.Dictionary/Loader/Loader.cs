using System;
using System.Collections.Generic;
using System.IO;

namespace DictionaryAssistantMVC.Dictionary.Loader
{
    public static class Loader
    {
        public static ActiveDictionary LoadDictionary(string pathToWordList)
        {
            List<string> readWords = new List<string>();
            using (StreamReader file = new StreamReader(pathToWordList))
            {
                while (!file.EndOfStream)
                {
                    readWords.Add(file.ReadLine());
                }
            }

            if (readWords.Count == 0)
            {
                throw new FileLoadException("No words were found in the dictionary file!", pathToWordList);
            }

            return new ActiveDictionary(readWords);
        }

        public static ActiveDictionary LoadDictionary(Stream wordListStream)
        {
            List<string> readWords = new List<string>();
            using (StreamReader file = new StreamReader(wordListStream))
            {
                while (!file.EndOfStream)
                {
                    readWords.Add(file.ReadLine());
                }
            }

            if (readWords.Count == 0)
            {
                throw new FileLoadException("No words were found in the dictionary file!", "wordListStream");
            }

            return new ActiveDictionary(readWords);
        }
    }
}
