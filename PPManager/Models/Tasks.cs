using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPManager.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public string? Title { get; set; }
        [MaxLength(50, ErrorMessage = "Max Length is 50 chrs")]
        public string? Desc { get; set; }
        public DateTime? DateLimit { get; set; }

        [Range(1, 3, ErrorMessage = "Value must be btw 1 & 3")]
        public int PriorityLevel { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }
    }


}
