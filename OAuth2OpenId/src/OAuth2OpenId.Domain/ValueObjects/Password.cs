using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace OAuth2OpenId.Domain.ValueObjects;

public class Password
{
    private Password(string hash)
    {
        PasswordHash = hash;
    }

    private Password()
    {
        
    }
    public string PasswordHash { get; init; }

    public static Password CreatePassword(string password)
    {
        if (!ValidatePassword(password))
            throw new Exception();
        var randomBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
    
        byte[] salt = randomBytes;
        using var argon2 = GetArgon2id(password, salt);

        var hash = argon2.GetBytes(16);
        var hashPlusSalt = new byte[hash.Length + salt.Length];
        Array.Copy(hash, 0, hashPlusSalt, 0, hash.Length);
        Array.Copy(salt, 0, hashPlusSalt, hash.Length, salt.Length);

        return new Password(Convert.ToBase64String(hashPlusSalt));
    }

    private static bool ValidatePassword(string password)
    {
        //todo: full fill this 
        return true;
    }
    
    public bool CheckPassword(string password)
    {
        byte[] hashPlusSalt = Convert.FromBase64String(PasswordHash);
        byte[] salt = new byte[16];
        byte[] hash = new byte[hashPlusSalt.Length - salt.Length];

        Array.Copy(hashPlusSalt, hash.Length, salt, 0, salt.Length);
        Array.Copy(hashPlusSalt, 0, hash, 0, hash.Length);

        using var argon2 = GetArgon2id(password, salt);

        var newHash = argon2.GetBytes(16);
        return newHash.SequenceEqual(hash);
    }

    private static Argon2id GetArgon2id(string password, byte[] salt)
    {
        return new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 1,
            MemorySize = 1024 * 46,
            Iterations = 1
        };
    }
    
}