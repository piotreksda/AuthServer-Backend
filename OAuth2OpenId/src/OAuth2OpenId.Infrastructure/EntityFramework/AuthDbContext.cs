using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.Configuration;

namespace OAuth2OpenId.Infrastructure.EntityFramework;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AccessTokenConfiguration());
        modelBuilder.ApplyConfiguration(new IdTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RedirectUriConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorizationCodeConfiguration());
        modelBuilder.ApplyConfiguration(new ClientGrandConfiguration());
        modelBuilder.ApplyConfiguration(new ClientScopeConfiguration());
        modelBuilder.ApplyConfiguration(new UserPermitConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceServerConfiguration());
        modelBuilder.ApplyConfiguration(new ResourceServersClientEnvironmentsConfiguration());
        modelBuilder.ApplyConfiguration(new ClientEnvironmentConfiguration());
    }
}