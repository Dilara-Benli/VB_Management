using System.ComponentModel.DataAnnotations;

namespace VB_api.Models
{
    public class LoginRequest
    {
        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string email { get; set; } 

        [Required]
        [MinLength(6)]
        public string password { get; set; } 
    }
}

