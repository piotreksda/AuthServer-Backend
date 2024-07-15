using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Id);

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);

        builder.HasOne<User>()
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId);
        
    }
}