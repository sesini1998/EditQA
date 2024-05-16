using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CapitalTest.Models
{
    public class Users
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Fisrt name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "User Role is required")]
        public required UserRole UserRole { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }

    public enum UserRole
    {
        Admin = 1,
        Candidate = 2
    }
}
