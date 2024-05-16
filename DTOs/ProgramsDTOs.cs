using CapitalTest.Models;
using System.ComponentModel.DataAnnotations;

namespace CapitalTest.DTOs
{
    public record ProgramDto
    {
        public Guid Id { get; init; }
        public required string ProgramTitle { get; init; }
        public required string ProgramDescription { get; init; }
        public ICollection<Questions> Questions { get; set; } = [];
    }

    public record class CreateProgramDto
    {
        [Required(ErrorMessage = "Program title is required")]
        [StringLength(100, ErrorMessage = "Program title must not exceed 100 characters")]
        public required string ProgramTitle { get; init; }
        [Required(ErrorMessage = "Program description is required")]
        [StringLength(500, ErrorMessage = "Program description must not exceed 500 characters")]
        public required string ProgramDescription { get; init; }
    }

    public record class UpdateProgramDto
    {
        public Guid Id { get; init; }
        [Required(ErrorMessage = "Program title is required")]
        [StringLength(100, ErrorMessage = "Program title must not exceed 100 characters")]
        public required string ProgramTitle { get; init; }
        [Required(ErrorMessage = "Program description is required")]
        [StringLength(500, ErrorMessage = "Program description must not exceed 500 characters")]
        public required string ProgramDescription { get; init; }
        public ICollection<Questions> Questions { get; set; } = [];
    }

}
