using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.AllowedScopes)
            .WithOne()
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ClientEnvironments)
            .WithOne()
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.AllowedGrantTypes)
            .WithOne()
            .HasForeignKey(x => x.ClientId);
    
        builder.HasMany<AuthorizationCode>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);
        
        builder.HasMany<AccessToken>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);
        
        builder.HasMany<IdToken>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);

        builder.HasMany<RefreshToken>()
            .WithOne()
            .HasForeignKey(x => x.ClientId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId);
    }
}