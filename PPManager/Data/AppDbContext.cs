using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PPManager.Models;

namespace PPManager.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Project { get; set; }
        public DbSet<Tasks> Task { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<TimerRecord> TimerRecord { get; set; }

    }
}
