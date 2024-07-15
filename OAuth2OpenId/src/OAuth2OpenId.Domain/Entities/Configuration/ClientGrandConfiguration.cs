using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.Configuration;

public class ClientGrandConfiguration : IEntityTypeConfiguration<ClientGrand>
{
    public void Configure(EntityTypeBuilder<ClientGrand> builder)
    {
        builder.ToTable("ClientGrands");

        builder.HasKey(x => x.Id);

        builder.HasOne<Client>()
            .WithMany(x => x.AllowedGrantTypes)
            .HasForeignKey(x => x.ClientId);
    }
}