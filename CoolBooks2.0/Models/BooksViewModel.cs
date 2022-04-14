using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    [NotMapped]
    public partial class BooksViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }

        public int BooksID { get; set; }
        //public string AuthorName { get; set; }
        //public string GenreName { get; set; }

        public List<string> AuthorName { get; set; }
        public List<string> GenreName { get; set; }

        public DateTime Created { get; set; }

        public List<int> AutorsId { get; set; }
        public List<int> GenresId { get; set; }

    
    }
}
