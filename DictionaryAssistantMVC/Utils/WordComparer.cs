using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DictionaryAssistantMVC.Models;

namespace DictionaryAssistantMVC.Utils
{
    public class WordComparer : IEqualityComparer<Word>
    {
        public bool Equals([AllowNull] Word x, [AllowNull] Word y)
        {
            return string.Equals(x?.TheWord, y?.TheWord);
        }

        public int GetHashCode([DisallowNull] Word obj)
        {
            return obj.TheWord.GetHashCode();
        }
    }
}
