using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using ResourceServer.Entites;

namespace ResourceServer;

public interface ISingUpService
{

}

public class SingUpService : ISingUpService
{
    public static async Task CreateOnSignUp(TicketReceivedContext ticketReceivedContext)
    {
        using var scope = ticketReceivedContext.HttpContext.RequestServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var userIdClaim = ticketReceivedContext.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        Console.WriteLine($"welcome User {userIdClaim?.Value}");

        var userId = Int32.Parse(userIdClaim.Value);
        var usersForTest = await dbContext.Users.ToListAsync();
        if (!dbContext.Users.Any(x => x.Id == userId))
        {
            var userToAdd = new User(userId, "test");
            await dbContext.Users.AddAsync(userToAdd);
            await dbContext.SaveChangesAsync();
        }
    }
}