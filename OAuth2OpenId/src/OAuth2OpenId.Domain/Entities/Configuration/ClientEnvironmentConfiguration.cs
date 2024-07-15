using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ClientEnvironmentConfiguration : IEntityTypeConfiguration<ClientEnvironment>
{
    public void Configure(EntityTypeBuilder<ClientEnvironment> builder)
    {
        builder.ToTable("ClientEnvironments").HasKey(x => x.Id);

        builder.HasMany(x => x.RedirectUris)
            .WithOne()
            .HasForeignKey(x => x.ClientEnvironmentId);

        builder.HasOne<Client>()
            .WithMany(x => x.ClientEnvironments)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany(x => x.ResourceServersClientEnvironments)
            .WithOne(x => x.ClientEnvironment)
            .HasForeignKey(x => x.ClientEnvironmentId);
    }
}