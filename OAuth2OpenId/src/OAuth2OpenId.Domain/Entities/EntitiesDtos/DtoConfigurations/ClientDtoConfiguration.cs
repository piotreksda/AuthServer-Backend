using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OAuth2OpenId.Domain.Entities.EntitiesDtos.DtoConfigurations;

public class ClientDtoConfiguration : IEntityTypeConfiguration<ClientDto>
{
    public void Configure(EntityTypeBuilder<ClientDto> builder)
    {
        builder.HasKey(x => x.Id);
    }
}