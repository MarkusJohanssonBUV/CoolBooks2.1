using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    [NotMapped]
    public class Roles
    {
        
        
        public int Id { get; set; }

        public string RoleName { get; set; }

        
    }
}
