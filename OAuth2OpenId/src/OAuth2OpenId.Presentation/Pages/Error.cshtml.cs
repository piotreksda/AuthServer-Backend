using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OAuth2OpenId.Presentation.Pages;

public class Error : PageModel
{
    public ErrorInputModel ErrorModel { get; private set; }
    public void OnGet([FromQuery] ErrorInputModel errorModel)
    {
        ErrorModel = errorModel;
    }
}

public class ErrorInputModel
{
    public string Message { get; init; }
    public string RedirectUri { get; init; }
}

