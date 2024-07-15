namespace OAuth2OpenId.Domain.Entities;

public class BaseEntity<T> where T : struct
{
    public T Id { get; init; }
}