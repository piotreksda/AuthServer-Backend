using System.Security.Cryptography;
using OAuth2OpenId.Application.Abstractions;

namespace OAuth2OpenId.Infrastructure.Services;

public class KeyManager : IKeyManager
{
    public KeyManager()
    {
        // OpenIdKey = RSA.Create();
        // if (File.Exists("openIdKey") && File.Exists("openIdKey.pub"))
        // {
        //     OpenIdKey.ImportRSAPrivateKey(File.ReadAllBytes("openIdKey"), out _);
        //     OpenIdKey.ImportRSAPublicKey(File.ReadAllBytes("openIdKey.pub"), out _);
        // }
        // else
        // {
        //     if (File.Exists("openIdKey"))
        //         File.Delete("openIdKey");
        //     if (File.Exists("openIdKey.pub"))
        //         File.Delete("openIdKey.pub");
        //     var privateKey = OpenIdKey.ExportRSAPrivateKey();
        //     File.WriteAllBytes("openIdKey", privateKey);
        //     var publicKey = OpenIdKey.ExportRSAPublicKey();
        //     File.WriteAllBytes("openIdKey.pub", publicKey);
        // }
        
        OpenIdKey = RSA.Create();
        if (File.Exists("openIdKey"))
        {
            OpenIdKey.ImportRSAPrivateKey(File.ReadAllBytes("openIdKey"), out _);
        }
        else
        {
            var privateKey = OpenIdKey.ExportRSAPrivateKey();
            File.WriteAllBytes("openIdKey", privateKey);
        }
    }
    
    public RSA OpenIdKey { get; init; }
}