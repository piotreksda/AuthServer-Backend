namespace ResourceServer.Entites;

public class User
{
    private User()
    {
        
    }

    public User(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; init; }
    public string Name { get; init; }
}