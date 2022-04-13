using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class Roles
    {
        [NotMapped]
        
        public int Id { get; set; }

        public string RoleName { get; set; }

        
    }
}
