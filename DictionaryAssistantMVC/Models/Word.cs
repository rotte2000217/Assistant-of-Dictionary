using System.ComponentModel.DataAnnotations;


namespace DictionaryAssistantMVC.Models
{
    public class Word
    {
        [Key]
        public int ID { get; set; }
        public string TheWord { get; set; }

        public int LetterID { get; set; }
        public Letter Letter { get; set; }
    }
}
