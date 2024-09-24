using PPManager.Models;

namespace PPManager.DTOs
{
    public class EditDTO
    {
        public string? Title { get; set; }
        public string? Desc { get; set; }
        public int? PriorityLevel { get; set; }
        public DateTime? DateLimit { get; set; }
    }
}
