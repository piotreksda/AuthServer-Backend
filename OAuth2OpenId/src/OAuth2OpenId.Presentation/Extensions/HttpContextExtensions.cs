using System.Security.Claims;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenIdPresentation.Extensions;

public static class HttpContextExtensions
{
    public static Result<int> GetUserId(this HttpContext httpContext)
    {
        if (!int.TryParse(httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                out int result))
        {
            return Result.Fail<int>("There is no userId in user claims");
        }
        return Result.Ok(result);
    }
}