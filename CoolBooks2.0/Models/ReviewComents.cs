using CoolBooks.Areas.Identity;

namespace CoolBooks.Models
{
    public class ReviewComents
    {
        public int ReviewComentsID { get; set; }
        public bool React { get; set; }
        public string Comments { get; set; }

        public int ReviewsID { get; set; }
        public Reviews Reviews { get; set; }
        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }
    }
}
