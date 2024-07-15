using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class AuthorizationCodeConfiguration : IEntityTypeConfiguration<AuthorizationCode>
{
    public void Configure(EntityTypeBuilder<AuthorizationCode> builder)
    {
        builder.ToTable("AuthorizationCodes");

        builder.HasKey(x => x.Id);

        builder.HasOne<User>()
            .WithMany(x => x.AuthorizationCodes)
            .HasForeignKey(x => x.UserId);

        builder.HasOne<RedirectUri>()
            .WithMany()
            .HasForeignKey(x => x.RedirectUriId);

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);

    }
}