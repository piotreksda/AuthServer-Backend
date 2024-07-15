namespace OAuth2OpenId.Application.Commands.Login;

public class LoginInputModel
{
    public string UserName { get; init; }
    public string Password { get; init; }
    public string? Continue { get; init; }
}