using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using DictionaryAssistantMVC.Dictionary;
using DictionaryAssistantMVC.Models;
using DictionaryAssistantMVC.Utils;

namespace DictionaryAssistantMVC.Contexts
{
    public class DictionaryContext : DbContext
    {
        public DictionaryContext(DbContextOptions<DictionaryContext> options) : base(options)
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Letter> Letters { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Word>()
                .HasIndex(w => w.TheWord)
                .IsUnique();
        }

        public int AddWordEntities(IEnumerable<Word> words, bool seeding)
        {
            int countAdded = 0;

            // Prevent duplicate words in `words` from being enumerated on
            foreach (var word in words.ToHashSet(new WordComparer()))
            {
                // `seeding` should only be true if being called from the SeedContext.
                if (seeding == false)
                {
                    bool alreadyInDatabase = this.Words.Where(w => w.TheWord == word.TheWord).Any();

                    if (alreadyInDatabase == false)
                    {
                        this.Words.Add(word);
                        countAdded += 1;
                    }
                }
                else
                {
                    this.Words.Add(word);
                    countAdded += 1;
                }
            }

            return countAdded;
        }

        public int AddWordEntities(IEnumerable<Word> words)
        {
            return AddWordEntities(words, false);
        }

        public ContextDictionary GetWordDictionaryForContext()
        {
            return new ContextDictionary(this);
        }

        public sealed class ContextDictionary : IWordDictionary
        {
            private readonly DictionaryContext parent;

            public ContextDictionary(DictionaryContext parent)
            {
                this.parent = parent;
            }

            public List<string> GetDictionaryWords()
            {
                return parent.Words.Select(w => w.TheWord).OrderBy(s => s[0]).ToList();
            }

            public List<string> GetDictionaryWordsEndingWith(char letter)
            {
                var words = parent.Words.Select(w => w.TheWord).ToList();

                return words.OrderBy(s => s.Last()).Where(s => s.Last().Equals(letter)).ToList();
            }

            public List<string> GetDictionaryWordsStartingWith(char letter)
            {
                int letterID = parent.Letters.Where(l => l.Character == letter).First().LetterID;

                var words = parent.Words.Where(w => w.LetterID == letterID).Select(w => w.TheWord).ToList();
                return words.OrderBy(s => s[0]).ToList();
            }

            public void SaveWordsForLettersEnding(char letter, List<string> words)
            {
                parent.GetService<ILogger>().LogWarning("Called SaveWordsForLettersEnding() in DbContext's ContextDictionary!");
                return;
            }

            public void SaveWordsForLetterStarting(char letter, List<string> words)
            {
                parent.GetService<ILogger>().LogWarning("Called SaveWordsForLettersStarting() in DbContext's ContextDictionary!");
                return;
            }
        }
    }
}
