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
        public bool? NewestBook { get; set; } 
        public bool MostLikedBook { get; set; } 
        public bool MostCommetedBook { get; set; } 
        public bool MostDislikedBook { get; set; }
        public int QuoteId { get; set; }
        public List<string> Quote { get; set; }
        public bool IsQuoteModerated { get; set; }
        public List<Quotes> Quotes { get; set; }

        public int BooksID { get; set; }
        //public string AuthorName { get; set; }
        //public string GenreName { get; set; }

        public List<string> AuthorName { get; set; }
        public List<string> GenreName { get; set; }
        public List<string> AllGenreName { get; set; }

        public List<Reviews>? Reviews { get; set; }
        public string ReviewText { get; set; }
        public string ReviewTitle { get; set; }
        public int ReviewRating { get; set; }
        public double? RatingAvg { get; set; }
        public string ReviewBookId { get; set; }
        public string ClientId { get; set; }    
        public List<int> RandomBooklist { get; set; }

       
        public int NumberOfReviews { get; set; }
        public List<string> UserName { get; set; }

        public DateTime? Created { get; set; }
        public ICollection<ReviewComents> ReviewComents { get; set; }
        public List<int> AutorsId { get; set; }
        public List<int> GenresId { get; set; }
        public List<int> UserId { get; set; }

    }
}
