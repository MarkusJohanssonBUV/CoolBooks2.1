using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class Quotes
    {
        [Key]
        public int QuoteId { get; set; }
        public string Quote { get; set; }
        public bool IsQuoteModerated { get; set; }

        public int? BookID { get; set; }
        [ForeignKey("BookID")]
        public Books Books { get; set; }

        public int AuthorID { get; set; }
        [ForeignKey("AuthorID")]
        public Authors Authors { get; set; }

    }
}
