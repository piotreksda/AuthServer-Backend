using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class RedirectUriConfiguration : IEntityTypeConfiguration<RedirectUri>
{
    public void Configure(EntityTypeBuilder<RedirectUri> builder)
    {
        builder.ToTable("RedirectUris");

        builder.HasKey(x => x.Id);

        builder.HasOne<ClientEnvironment>()
            .WithMany(x => x.RedirectUris)
            .HasForeignKey(x => x.ClientEnvironmentId);

        builder.HasMany<AuthorizationCode>()
            .WithOne()
            .HasForeignKey(x => x.RedirectUriId);
    }
}