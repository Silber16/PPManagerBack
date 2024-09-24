using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPManager.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        public string Title { get; set; }

        [MaxLength(50, ErrorMessage = "Max Length is 50 chrs")]
        public string? Desc { get; set; }
        public IList<string>? Link { get; set; }
        public int ProjectId { get; set; }

        [JsonIgnore]
        public Project? Project { get; set; }  
    }
}
