using Microsoft.EntityFrameworkCore;
using TalktifAPI.Model;

namespace TalktifAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        public DbSet<User> Users {get; set;}
        public DbSet<Report> Reports { get; set; }
        public DbSet<Messages> Messages { get; set; } 
        public DbSet<ChatRoom> ChatRooms { get; set; }
        
        
    }
}