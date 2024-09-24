using Microsoft.AspNetCore.Identity;

namespace PPManager.Models
{
    public class MyUser : IdentityUser
    {
        public ICollection<Project> projetcts { get; set; } = new List<Project>();
    }
}
