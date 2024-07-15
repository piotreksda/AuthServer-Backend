using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace OAuth2OpenId.Infrastructure.EntityFramework;

public class AuthDbContextInitializer
{
    private readonly AuthDbContext _context;

    public AuthDbContextInitializer(AuthDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
                await _context.Database.MigrateAsync();
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}