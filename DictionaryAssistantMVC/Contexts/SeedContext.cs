using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using DictionaryAssistantMVC.Dictionary;
using DictionaryAssistantMVC.Dictionary.Loader;
using DictionaryAssistantMVC.Models;

namespace DictionaryAssistantMVC.Contexts
{
    public static class SeedContext
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<DictionaryContext>();
            context.Database.EnsureCreated();

            if (!context.Letters.Any() && !context.Words.Any())
            {
                var dictionary = Loader.LoadDictionary(Path.Combine(Directory.GetCurrentDirectory(), "seed_words.txt"));

                foreach (var dictLetter in dictionary.GetAllDictionaryLetters())
                {
                    // Create Letter Entities
                    var createdLetter = context.Letters.Add(
                        new Letter()
                        {
                            Character = dictLetter.Letter,
                            AverageCharacters = dictLetter.AverageCharacterCount,
                            CountBeginningWith = dictLetter.NumberWordsBeginningWith,
                            CountEndingWith = dictLetter.NumberWordsEndingWith
                        });

                    // Create Associated Word Entities
                    var createdWords =
                        dictLetter.WordsBeginningWith.Select(w => new Word() { TheWord = w, Letter = createdLetter.Entity });
                    
                    context.Words.AddRange(createdWords);
                }

                context.SaveChanges();
            }
        }
    }
}
