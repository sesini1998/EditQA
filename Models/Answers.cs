using System.ComponentModel;

namespace CapitalTest.Models
{
    public class Answers
    {
        public required Guid Id { get; set; }
        public required List<QuestionDetails> Questions { get; set; }
        public required UserDetails User { get; set; }
    }

    public class QuestionDetails
    {
        public Guid Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string QuestionTitle { get; set; }
        public bool Visibility { get; set; }
        public required string GivenAnswer { get; set; }

    }

    public class UserDetails
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
