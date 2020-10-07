using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using DictionaryAssistantMVC.Dictionary;

namespace DictionaryAssistantMVC.Dictionary.Tests
{
    [TestFixture]
    public class DictionaryLetterTests
    {
        internal static IEnumerable<char> AlphabetChars = new Alphabet();

        [TestCaseSource("AlphabetChars")]
        public void AverageCharacterCountEmptyDictionaryTest(char alphabetLetter)
        {
            var eachLetter = DictionaryLetter.InitializeDictionaryLetter(alphabetLetter, new MockDictionary(new string[0]));

            Assert.AreEqual(0, eachLetter.AverageCharacterCount);
        }

        [TestCase('a', 5)]
        [TestCase('s', 5)]
        public void NumberBeginningWithLetterTest(char letter, int numberBegin)
        {
            var letterWhat = DictionaryLetter.InitializeDictionaryLetter(letter, new MockDictionary());

            Assert.AreEqual(numberBegin, letterWhat.NumberWordsBeginningWith);
        }

        [TestCase('a', 0)]
        [TestCase('s', 5)]
        public void NumberEndingWithLetterTest(char letter, int numberEnding)
        {
            var letterWhat = DictionaryLetter.InitializeDictionaryLetter(letter, new MockDictionary());

            Assert.AreEqual(numberEnding, letterWhat.NumberWordsEndingWith);
        }

        [TestCase('a', 10)]
        [TestCase('s', 7)]
        public void AverageCharacterCountTest(char letter, int avgCount)
        {
            var letterWhat = DictionaryLetter.InitializeDictionaryLetter(letter, new MockDictionary());

            Assert.AreEqual(avgCount, letterWhat.AverageCharacterCount);
        }

        [TestCase('a')]
        [TestCase('s')]
        public void WordListLengthAndPropertyEqualTest(char letter)
        {
            var letterWhat = DictionaryLetter.InitializeDictionaryLetter(letter, new MockDictionary());

            Assert.AreEqual(letterWhat.NumberWordsBeginningWith, letterWhat.WordsBeginningWith.Count);
        }

        [Test]
        public void LetterAWordsInWordListTest()
        {
            var letterA = DictionaryLetter.InitializeDictionaryLetter('a', new MockDictionary());
            string[] letterAWords = new string[]
            {
                "abbreviations",
                "abdicates",
                "abductions",
                "abductors",
                "abilities"
            };

            foreach (string s in letterAWords)
            {
                Assert.IsTrue(letterA.WordsBeginningWith.Contains(s));
            }
        }

        [Test]
        public void LetterALongestWordsTest()
        {
            var letterA = DictionaryLetter.InitializeDictionaryLetter('a', new MockDictionary());

            var longestWords = letterA.GetLongestWords();

            Assert.AreEqual(2, longestWords.Count);
            Assert.AreEqual("abbreviations", longestWords.First());
            Assert.AreEqual("abductions", longestWords.Last());
        }

        [Test]
        public void LetterAShortestWordsTest()
        {
            var letterA = DictionaryLetter.InitializeDictionaryLetter('a', new MockDictionary());

            var shortestWords = letterA.GetShortestWords();

            Assert.AreEqual(3, shortestWords.Count);
        }

        [Test]
        public void LetterSWordsInWordListTest()
        {
            var letterS = DictionaryLetter.InitializeDictionaryLetter('s', new MockDictionary());
            string[] letterSWords = new string[]
            {
                "stammer",
                "stamp",
                "stance",
                "standard",
                "standstill"
            };

            foreach (string s in letterSWords)
            {
                Assert.IsTrue(letterS.WordsBeginningWith.Contains(s));
            }
        }
    }
}
