using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
{
    public void Configure(EntityTypeBuilder<ClientScope> builder)
    {
        builder.ToTable("ClientScopes");

        builder.HasKey(x => x.Id);
        
        builder.HasOne<Client>()
            .WithMany(x => x.AllowedScopes)
            .HasForeignKey(x => x.ClientId);

        builder.HasMany<UserPermit>()
            .WithOne(x => x.ClientScope)
            .HasForeignKey(x => x.ClientScopeId);
    }
}