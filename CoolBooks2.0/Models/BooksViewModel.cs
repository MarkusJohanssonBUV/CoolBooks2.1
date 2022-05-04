
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    [NotMapped]
    public partial class BooksViewModel
    {


        //OBS Kolla på att göra om denna så att den innhåller listor av Books, Authors osv, använd MVC-Movie som exempel!!
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string ISBN { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string ImagePath { get; set; }


        public bool IsDeleted { get; set; }

        public bool? IsBookOfTheWeek { get; set; }
        public bool? NewestBook { get; set; } 
        public bool MostLikedBook { get; set; } 
        public bool MostCommetedBook { get; set; } 
        public bool MostDislikedBook { get; set; }
        //public int QuoteId { get; set; }
        public List<string> Quote { get; set; }
        //public bool IsQuoteModerated { get; set; }
        public List<Quotes> Quotes { get; set; }

        public int BooksID { get; set; }
        //public string AuthorName { get; set; }
        //public string GenreName { get; set; }

        public List<string> AuthorName { get; set; }
        public List<string> GenreName { get; set; }
        //public List<string> AllGenreName { get; set; }

        public List<Reviews>? Reviews { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string ReviewText { get; set; }
        //public string ReviewTitle { get; set; }

      
        public int ReviewRating { get; set; }
        public double? RatingAvg { get; set; }
        //public string ReviewBookId { get; set; }
        //public string ClientId { get; set; }    
        //public List<int> RandomBooklist { get; set; }
        //public List<DateTime>? ReviewCreated { get; set; }

        //public int NumberOfReviews { get; set; }
        public List<string> UserName { get; set; }

        public DateTime? Created { get; set; }

        public List<int> AutorsId { get; set; }
        public List<int> GenresId { get; set; }
        //public List<int> UserId { get; set; }

    }
}
