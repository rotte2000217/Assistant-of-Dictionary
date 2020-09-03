using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryAssistantMVC.Dictionary
{
    public class LetterStatistics
    {
        public char Letter { get; set; }
        public int AverageCharacterCount { get; set; }
        public int NumberWordsBeginningWith { get; set; }
        public int NumberWordsEndingWith { get; set; }
    }
}
