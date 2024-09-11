using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace OCR_E_gov.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Username { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long.")]
        public required string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; set; }
        public required string FullName { get; set; }

        //public required ICollection<Company_Table> Companies { get; set; }
    }
}
