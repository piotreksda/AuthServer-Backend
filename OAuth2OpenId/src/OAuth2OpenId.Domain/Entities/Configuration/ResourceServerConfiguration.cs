using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ResourceServerConfiguration : IEntityTypeConfiguration<ResourceServer>
{
    public void Configure(EntityTypeBuilder<ResourceServer> builder)
    {
        builder.ToTable("ResourceServers").HasKey(x => x.Id);

        builder.HasMany(x => x.ResourceServersClientEnvironments)
            .WithOne(x => x.ResourceServer)
            .HasForeignKey(x => x.ResourceServerId);
    }
}