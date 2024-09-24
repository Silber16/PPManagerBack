using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPManager.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Desc { get; set; }
        public string? UserId { get; set; }

        [JsonIgnore]
        public MyUser? User { get; set; }
        public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
