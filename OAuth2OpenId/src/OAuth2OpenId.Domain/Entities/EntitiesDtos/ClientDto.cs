namespace OAuth2OpenId.Domain.Entities.EntitiesDtos;

public record ClientDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
};