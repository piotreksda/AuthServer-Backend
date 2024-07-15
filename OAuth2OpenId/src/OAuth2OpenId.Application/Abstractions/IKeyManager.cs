using System.Security.Cryptography;

namespace OAuth2OpenId.Application.Abstractions;

public interface IKeyManager
{
    public RSA OpenIdKey { get; init; }
}