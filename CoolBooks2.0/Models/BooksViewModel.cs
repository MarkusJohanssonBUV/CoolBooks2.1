using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    [NotMapped]
    public partial class BooksViewModel
    {

        //OBS Kolla på att göra om denna så att den innhåller listor av Books, Authors osv, använd MVC-Movie som exempel!!
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }


        public bool IsDeleted { get; set; }

        public bool? IsBookOfTheWeek { get; set; }
        //public bool? NewestBook { get; set; } //ny ej (add-migration) än
        //public bool MostLikedBook { get; set; } //ny ej (add-migration) än
        //public bool MostCommetedBook {get; set; } //ny ej (add-migration) än
        //public bool MostDislikedBook {get; set; } // ny ej (add-migration) än

        public int BooksID { get; set; }
        //public string AuthorName { get; set; }
        //public string GenreName { get; set; }

        public List<Books>? Books { get; set; }

        public List<string> AuthorName { get; set; }
        public List<string> GenreName { get; set; }
        public List<string> AllGenreName { get; set; }

        public SelectList AuthorNameSelect { get; set; }
        public SelectList GenreNameSelect { get; set; }
        public List<string> UserName { get; set; }

        public DateTime? Created { get; set; }

        public List<int> AutorsId { get; set; }
        public List<int> GenresId { get; set; }
        public List<int> UserId { get; set; }

    }
}
