using CoolBooks.Areas.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class ReviewComents
    {
        public int ReviewComentsID { get; set; }
        public bool React { get; set; }

        [StringLength(1000, MinimumLength = 3)]
        [Required]
        public string Comments { get; set; }
        public int ReviewsID { get; set; }
        public Reviews Reviews { get; set; }
        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }
    }
}
