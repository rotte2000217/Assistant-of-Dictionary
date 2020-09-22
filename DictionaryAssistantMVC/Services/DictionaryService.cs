using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DictionaryAssistantMVC.Contexts;
using DictionaryAssistantMVC.Models;
using DictionaryAssistantMVC.Dictionary;
using System.IO;
using DictionaryAssistantMVC.Dictionary.Loader;

namespace DictionaryAssistantMVC.Services
{
    public interface IDictionaryService
    {
        Task<int> AddWordsToDictionary(Stream wordStream);
        Task<List<LetterStatistics>> GetTopLettersByCount(int howMany);
        Task<List<LetterStatistics>> GetAllLettersByCount();
        Task<List<LetterStatistics>> GetAllLetters();
        Task<Dictionary<char, List<string>>> GetLongestWords(int howMany);
        Task<Dictionary<char, List<string>>> GetShortestWords(int howMany);
    }

    public class DictionaryService : IDictionaryService
    {
        private readonly DictionaryContext context;

        public DictionaryService(DictionaryContext context)
        {
            this.context = context;
        }

        public async Task<int> AddWordsToDictionary(Stream wordStream)
        {
            int wordsAdded = 0;

            // Dictionary corresponding to the to-be-added wordlist
            ActiveDictionary addDictionary;
            try
            {
                addDictionary = Loader.LoadDictionary(wordStream);
            }
            catch (FileLoadException)
            {
                // Empty, Abort
                return 0;
            }

            // Dictionary corresponding to the in-database wordlist
            var dbDictionary = context.GetWordDictionaryForContext();

            var lettersToUpdate = addDictionary.GetAllDictionaryLetters().Select(dl => dl.Letter).ToList();

            // Get the actual entities from the DB
            List<Letter> letters =
                await context.Letters.Where(letter => lettersToUpdate.Contains(letter.Character))
                    .OrderBy(letter => letter.Character)
                    .ToListAsync();

            try
            {
                foreach (var letter in letters)
                {
                    var addTheseWords = addDictionary.GetDictionaryWordsStartingWith(letter.Character)
                        .Select(word => new Word() { TheWord = word, Letter = letter });

                    wordsAdded += addTheseWords.Count();
                    context.Words.AddRange(addTheseWords);
                }
            }
            finally
            {
                await context.SaveChangesAsync();
            }

            // Changes must be saved prior to getting updated states. fml -.-
            try
            {
                foreach (var letter in letters)
                {
                    // Update Letter Statistics
                    var updatedLetterStats = DictionaryLetter.InitializeDictionaryLetter(letter.Character, dbDictionary);
                    letter.AverageCharacters = updatedLetterStats.AverageCharacterCount;
                    letter.CountBeginningWith = updatedLetterStats.NumberWordsBeginningWith;
                    letter.CountEndingWith = updatedLetterStats.NumberWordsEndingWith;
                    context.Letters.Update(letter);
                }
            }
            finally
            {
                await context.SaveChangesAsync();
            }

            return wordsAdded;
        }

        public async Task<List<LetterStatistics>> GetAllLettersByCount()
        {
            return await context.Letters.OrderByDescending(l => l.CountBeginningWith)
                .Select(l => new LetterStatistics()
                {
                    Letter = l.Character,
                    AverageCharacterCount = l.AverageCharacters,
                    NumberWordsBeginningWith = l.CountBeginningWith,
                    NumberWordsEndingWith = l.CountEndingWith
                })
                .ToListAsync();
        }

        public async Task<List<LetterStatistics>> GetTopLettersByCount(int howMany)
        {
            return await context.Letters.OrderByDescending(l => l.CountBeginningWith)
                .Take(howMany)
                .Select(l => new LetterStatistics()
                {
                    Letter = l.Character,
                    AverageCharacterCount = l.AverageCharacters,
                    NumberWordsBeginningWith = l.CountBeginningWith,
                    NumberWordsEndingWith = l.CountEndingWith
                })
                .ToListAsync();
        }

        public async Task<List<LetterStatistics>> GetAllLetters()
        {
            return await context.Letters.OrderBy(l => l.Character)
                .Select(l => new LetterStatistics()
                {
                    Letter = l.Character,
                    AverageCharacterCount = l.AverageCharacters,
                    NumberWordsBeginningWith = l.CountBeginningWith,
                    NumberWordsEndingWith = l.CountEndingWith
                })
                .ToListAsync();
        }

        public async Task<Dictionary<char, List<string>>> GetLongestWords(int howMany)
        {
            Dictionary<char, List<string>> longestWords = new Dictionary<char, List<string>>();

            foreach (char c in new Alphabet())
            {
                var letter = await context.Letters.Where(l => l.Character == c).FirstAsync();
                int average = letter.AverageCharacters;
                int id = letter.LetterID;

                longestWords.Add(c, 
                    await context.Words.Where(w => w.LetterID == id)
                        .Where(w => w.TheWord.Length >= average)
                        .Take(howMany)
                        .Select(w => w.TheWord)
                        .ToListAsync()
                );
            }

            return longestWords;
        }

        public async Task<Dictionary<char, List<string>>> GetShortestWords(int howMany)
        {
            Dictionary<char, List<string>> shortestWords = new Dictionary<char, List<string>>();

            foreach (char c in new Alphabet())
            {
                var letter = await context.Letters.Where(l => l.Character == c).FirstAsync();
                int average = letter.AverageCharacters;
                int id = letter.LetterID;

                shortestWords.Add(c,
                    await context.Words.Where(w => w.LetterID == id)
                        .Where(w => w.TheWord.Length < average)
                        .Take(howMany)
                        .Select(w => w.TheWord)
                        .ToListAsync()
                );
            }

            return shortestWords;
        }
    }
}
