using Microsoft.EntityFrameworkCore;
using TalktifAPI.Model;

namespace TalktifAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<User> UserItems {get; set;}
        public DbSet<Report> ReportItems { get; set; }
        public DbSet<Messages> MessagesItems { get; set; } 
    }
}