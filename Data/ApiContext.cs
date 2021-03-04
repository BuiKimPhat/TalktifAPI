using Microsoft.EntityFrameworkCore;
using TalktifAPI.Model;

namespace TalktifAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<User> User {get; set;}
        public DbSet<Report> Report { get; set; }
        public DbSet<Messages> Message { get; set; } 
        public DbSet<ChatRoom> Chat_Room { get; set; }
        
        
    }
}