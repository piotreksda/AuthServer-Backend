using Microsoft.EntityFrameworkCore;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Entities.Configuration;
using OAuth2OpenId.Domain.Entities.EntitiesDtos;
using OAuth2OpenId.Domain.Entities.EntitiesDtos.DtoConfigurations;

namespace OAuth2OpenId.Infrastructure.EntityFramework;

public class AuthReadOnlyDbContext : DbContext
{
    public AuthReadOnlyDbContext(DbContextOptions<AuthReadOnlyDbContext> options) : base(options)
    {
        
    }

    public DbSet<ClientDto> Clients { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientDtoConfiguration());
    }
}