using Microsoft.EntityFrameworkCore;
using DictionaryAssistantMVC.Models;


namespace DictionaryAssistantMVC.Contexts
{
    public class DictionaryContext : DbContext
    {
        public DictionaryContext(DbContextOptions<DictionaryContext> options) : base(options)
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Letter> Letters { get; set; }
    }
}
