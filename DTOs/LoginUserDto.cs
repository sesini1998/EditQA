using System.ComponentModel.DataAnnotations;
using CapitalTest.Models;

namespace CapitalTest.DTOs
{
    public class LoginUserDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }

    public class LoggedUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole role { get; set; }
        public string token { get; set; }
    }
}
