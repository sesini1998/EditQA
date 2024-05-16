using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CapitalTest.Models
{
    public class Questions
    {
        [JsonProperty("id")]
        public required Guid Id { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required string QuestionTitle { get; set; }
        [DefaultValue(true)]
        public bool Visibility { get; set; }
        public Guid ProgramId { get; set; }
    }

    public enum QuestionType
    {
        Paragraph = 1,
        YesNo = 2,
        Dropdown = 3,
        MultipleChoice = 4,
        Date = 5,
        Number = 6
    }
}
