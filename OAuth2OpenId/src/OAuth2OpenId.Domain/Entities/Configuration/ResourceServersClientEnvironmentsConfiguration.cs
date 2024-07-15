using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ResourceServersClientEnvironmentsConfiguration : IEntityTypeConfiguration<ResourceServersClientEnvironments>
{
    public void Configure(EntityTypeBuilder<ResourceServersClientEnvironments> builder)
    {
        builder.ToTable("ResourceServersClientEnvironments")
            .HasKey(x => new { x.ResourceServerId, x.ClientEnvironmentId });

        builder.HasOne(x => x.ClientEnvironment)
            .WithMany(x => x.ResourceServersClientEnvironments)
            .HasForeignKey(x => x.ClientEnvironmentId);

        builder.HasOne(x => x.ResourceServer)
            .WithMany(x => x.ResourceServersClientEnvironments)
            .HasForeignKey(x => x.ResourceServerId);
    }
}