using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.ComplexProperty(x => x.Password);
        // builder.has

        builder.HasMany(x => x.AccessTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId);
        
        builder.HasMany(x => x.IdTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.AuthorizationCodes)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.UserPermits)
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasMany<Client>()
            .WithOne()
            .HasForeignKey(x => x.OwnerId);
    }
}