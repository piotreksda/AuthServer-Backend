namespace OAuth2OpenId.Domain.Configurations;

public record DatabaseOptions
{
    public static readonly string Section = "Database";

    public string Host { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Port { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Keepalive { get; init; } = string.Empty;

    public void Validate()
    {
        if (string.IsNullOrEmpty(Host) ||
            string.IsNullOrEmpty(Name) ||
            string.IsNullOrEmpty(Port) ||
            string.IsNullOrEmpty(User) ||
            string.IsNullOrEmpty(Password) ||
            string.IsNullOrEmpty(Keepalive))
        {
            throw new ArgumentNullException(Section);
        }
    }
}