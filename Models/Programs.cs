using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace CapitalTest.Models
{
    public class Programs
    {
        [JsonProperty("id")]
        public required Guid Id { get; set; }
        public required string ProgramTitle { get; set; }
        public required string ProgramDescription { get; set; }
        public ICollection<Questions> Questions { get; set; } = [];
    }
}
