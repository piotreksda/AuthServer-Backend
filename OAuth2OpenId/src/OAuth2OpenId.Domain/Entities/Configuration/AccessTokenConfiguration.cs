using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.ToTable("AccessTokens");

        builder.HasKey(x => x.Id);
        
        builder.HasOne<User>()
            .WithMany(x => x.AccessTokens)
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);
    }
}