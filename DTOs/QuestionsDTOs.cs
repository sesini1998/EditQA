using CapitalTest.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CapitalTest.DTOs
{
    public record QuestionDto
    {
        public Guid Id { get; init; }
        public QuestionType QuestionType { get; init; }
        public required string QuestionTitle { get; init; }
        public bool Visibility { get; init; }
        public Guid ProgramId { get; init; }
    }

    public record class CreateQuestionDto
    {
        [Required]
        public QuestionType QuestionType { get; init; }
        [Required(ErrorMessage = "Question Title is required")]
        [StringLength(150, ErrorMessage = "Question Title must not exceed 150 characters")]
        public required string QuestionTitle { get; init; }
        [Required]
        [DefaultValue(true)]
        public bool Visibility { get; init; }
        [Required]
        public Guid ProgramId { get; init; }
    }

    public record class UpdateQuestionDto
    {
        public Guid Id { get; init; }
        [Required]
        public QuestionType QuestionType { get; init; }
        [Required(ErrorMessage = "Question Title is required")]
        [StringLength(150, ErrorMessage = "Question Title must not exceed 150 characters")]
        public required string QuestionTitle { get; init; }
        [Required]
        public bool Visibility { get; init; }
        [Required]
        public Guid ProgramId { get; init; }
    }

}
