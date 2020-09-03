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
        Task<List<LetterStatistics>> GetTopLetters(int howMany);
        Task<List<LetterStatistics>> GetAllLetters();
    }

    public class DictionaryService : IDictionaryService
    {
        private readonly DictionaryContext context;

        public DictionaryService(DictionaryContext context)
        {
            this.context = context;
        }

        public async Task<List<LetterStatistics>> GetAllLetters()
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

        public async Task<List<LetterStatistics>> GetTopLetters(int howMany)
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
    }
}
