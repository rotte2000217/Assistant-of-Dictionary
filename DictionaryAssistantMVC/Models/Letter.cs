using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DictionaryAssistantMVC.Models
{
    public class Letter
    {
        [Key]
        public int LetterID { get; set; }

        public char Character { get; set; }
        public int AverageCharacters { get; set; }
        public int CountBeginningWith { get; set; }
        public int CountEndingWith { get; set; }

        public virtual ICollection<Word> Word { get; set; }
    }
}
