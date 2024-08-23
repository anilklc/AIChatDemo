using AIChatDemo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AIChatDemo.API.Context
{
    public class ChatDBContext : DbContext
    {
        public ChatDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
                
    }
}
