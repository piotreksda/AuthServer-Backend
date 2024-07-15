using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Token.Strategies;

public interface IStrategy
{
    public Task<Result<AuthenticateForTokenResult>> Validate();
}

public record AuthenticateForTokenResult
{
    public User User { get; init; } 
    public Guid ClientId { get; init; }
    public string Scopes { get; init; }
    public string? Nonce { get; init; }
}