using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DictionaryAssistantMVC.Contexts;
using DictionaryAssistantMVC.Models;
using DictionaryAssistantMVC.Dictionary;


namespace DictionaryAssistantMVC.Services
{
    public interface IDictionaryService
    {
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
