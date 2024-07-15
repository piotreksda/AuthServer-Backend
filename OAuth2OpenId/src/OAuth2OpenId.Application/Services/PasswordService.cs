using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace OAuth2OpenId.Application.Services;

public class PasswordOptions
{
    public int MinLength { get; init; } = 8;
    public bool NeedSpecialCharacters { get; init; } = false;
    public PasswordAlgorithmOptions PasswordAlgorithmOptions { get; init; } = default!;
}
public class PasswordAlgorithmOptions
{
    public int DegreeOfParallelism { get; init; } = 2;
    public int MemorySize { get; init; } = 1024 * 256;
    public int Iterations { get; init; } = 3;
}

public class PasswordService
{
    private readonly PasswordOptions _options;

    public PasswordService(PasswordOptions options)
    {
        _options = options;
    }

    private string CreatePasswordHash(string password)
    {
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

        return Convert.ToBase64String(hashPlusSalt);
    }

    private bool ValidatePassword(string password)
    {
        return true;
    }
    
    
    private bool CheckPassword(string password, string storedHashPlusSalt)
    {
        byte[] hashPlusSalt = Convert.FromBase64String(storedHashPlusSalt);
        byte[] salt = new byte[16];
        byte[] hash = new byte[hashPlusSalt.Length - salt.Length];

        Array.Copy(hashPlusSalt, hash.Length, salt, 0, salt.Length);
        Array.Copy(hashPlusSalt, 0, hash, 0, hash.Length);

        using var argon2 = GetArgon2id(password, salt);

        var newHash = argon2.GetBytes(16);
        return newHash.SequenceEqual(hash);
    }

    private Argon2id GetArgon2id(string password, byte[] salt)
    {
        return new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = _options.PasswordAlgorithmOptions.DegreeOfParallelism,
            MemorySize = _options.PasswordAlgorithmOptions.MemorySize,
            Iterations = _options.PasswordAlgorithmOptions.Iterations
        };
    }
}