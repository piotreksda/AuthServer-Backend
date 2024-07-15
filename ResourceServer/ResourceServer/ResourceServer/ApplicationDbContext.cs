using Microsoft.EntityFrameworkCore;
using ResourceServer.Entites;

namespace ResourceServer;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; } 
}