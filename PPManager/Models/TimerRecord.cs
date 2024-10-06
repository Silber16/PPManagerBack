using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPManager.Models
{
    public class TimerRecord
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? UserId {  get; set; }

        [JsonIgnore]
        public MyUser? User { get; set; }
    }
}
