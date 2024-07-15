using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class IdTokenConfiguration : IEntityTypeConfiguration<IdToken>
{
    public void Configure(EntityTypeBuilder<IdToken> builder)
    {
        builder.ToTable("IdTokens");

        builder.HasKey(x => x.Id);
        
        builder.HasOne<User>()
            .WithMany(x => x.IdTokens)
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);
    }
}