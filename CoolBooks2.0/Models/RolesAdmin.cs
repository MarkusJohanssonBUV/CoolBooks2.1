using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    [NotMapped]
    public class RolesAdmin
    {
        
        
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string RoleName { get; set; }

        
    }
}
