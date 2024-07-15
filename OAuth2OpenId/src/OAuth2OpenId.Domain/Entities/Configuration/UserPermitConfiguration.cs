using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class UserPermitConfiguration : IEntityTypeConfiguration<UserPermit>
{
    public void Configure(EntityTypeBuilder<UserPermit> builder)
    {
        builder.ToTable("UserPermits");

        builder.HasKey(x => new { x.UserId, x.ClientScopeId });

        builder.HasOne<User>()
            .WithMany(x => x.UserPermits)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.ClientScope)
            .WithMany()
            .HasForeignKey(x => x.ClientScopeId);
    }
}