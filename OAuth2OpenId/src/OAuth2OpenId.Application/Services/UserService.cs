using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using OAuth2OpenId.Application.Services.Interfaces;
using OAuth2OpenId.Domain.Entities;

namespace OAuth2OpenId.Application.Services;

public class UserServiceOptions
{
    public IEmailSender? EmailSender { get; init; } = null;
    public bool RequireEmailConfirmation { get; init; } = false;
    // public PasswordOptions PasswordOptions { get; init; } = default!;
    public MultiFactorAuthenticationOptions MultiFactorAuthenticationOptions { get; init; } = default!;
}



public class MultiFactorAuthenticationOptions
{
    public bool isMFARequired = false;
    public Dictionary<string, IFactorAuthenticator> FactorTypes { get; init; } = new ();
    
}

public interface IFactorAuthenticator
{
    public void Validate();
}

public class UserService 
{
    private readonly UserServiceOptions _options;

    public UserService(UserServiceOptions options)
    {
        _options = options;
    }

    public async Task<User> CreateAsync(string userName, string email, string password)
    {
        throw new NotImplementedException();
        // bool passwordIsValid = ValidatePassword(password);
        // if (passwordIsValid)
        //     throw new Exception(); //todo: create smart exception
        // string passwordHash = CreatePasswordHash(password); 
        //
        // //todo: consider what about required multi factor authorization
        // User user = new User(userName, email, passwordHash, false);
        //
        // if (_options.RequireEmailConfirmation)
        // {
        //     if (_options.EmailSender is null)
        //         throw new ArgumentNullException(nameof(_options.EmailSender));
        //     string emailConfirmationCode = CreateSecureRandomString();
        //     //todo: save in db
        //     await _options.EmailSender.SendEmailConfirmationEmailAsync(emailConfirmationCode);
        // }
        //
        // return user;
    }
    
    public async Task ChangePassword(User user, string password)
    {
        // bool passwordIsValid = ValidatePassword(password);
        //
        // string passwordHash = CreatePasswordHash(password);
        //
        // user.UpdatePasswordHash(password);
        //
        // if (_options.EmailSender is null)
        //     throw new ArgumentNullException(nameof(_options.EmailSender));
        // string emailConfirmationCode = CreateSecureRandomString();
        // //todo: save in db
        // await _options.EmailSender.SendPasswordResetEmailAsync(emailConfirmationCode);
    }
    

    

    private string CreateSecureRandomString()
    {
        var randomBytes = new byte[128];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}