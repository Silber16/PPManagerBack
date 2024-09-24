
using PPManager.Models;
using System.ComponentModel.DataAnnotations;

namespace PPManager.ViewModels
{
    public class LoginDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
