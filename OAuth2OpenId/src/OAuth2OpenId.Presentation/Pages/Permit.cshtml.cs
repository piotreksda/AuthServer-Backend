using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Models;
using OAuth2OpenIdPresentation.Extensions;

namespace OAuth2OpenId.Presentation.Pages;

public class Permit : PageModel
{
    private readonly IClientRepository _repository;
    public Permit(IClientRepository repository)
    {
        _repository = repository;
    }

    public List<ClientScopeViewModel> ClientScopes { get; private set; }
    public List<string> InputScopes { get; private set; }
    
    public string Continue { get; private set; }

    public async Task<IActionResult> OnGetAsync([FromQuery] PermitInputModel permitModel)
    {
        if (HttpContext.User.Identity?.IsAuthenticated == false)
        {
            string continueEncoded = System.Web.HttpUtility.UrlEncode(HttpContext.Request.GetEncodedUrl());
            return new RedirectResult($"/Login?continue={continueEncoded}");
        }

        Result<int> userIdResult = HttpContext.GetUserId();
        if (userIdResult.IsFailure)
        {
            return RedirectToPage("/Error");
        }

        throw new NotImplementedException();
        // Result<List<ClientScopeViewModel>> result = await _repository.GetClientScopes(permitModel.ClientId, userIdResult.Value, CancellationToken.None);
        // if (result.IsFailure)
        // {
        //     return RedirectToPage("/Error");
        // }
        //
        // ClientScopes = result.Value!;
        // InputScopes = permitModel.Scopes;
        //
        // // Check if all input scopes are present in the client scopes
        // if (!InputScopes.All(scope => ClientScopes.Any(cs => cs.ScopeName == scope)))
        // {
        //     return RedirectToPage("/Error");
        // }
        //
        // Continue = permitModel.Continue;

        return Page();
    }

    public async Task<IActionResult> OnPostAcceptAsync(List<int> scopes, string continueWith)
    {
        //todo: save to database
        //todo: redirect with continue
        var userId = HttpContext.GetUserId();
        if (userId.IsFailure)
            return RedirectToPage("/Error");
        //todo: change to match clean architecture (logic to commands not in repository :sadge:)
        // var result = await _repository.ChangeUserClientScopes(userId.Value!, scopes, CancellationToken.None);
        // if (result.IsFailure)
            return RedirectToPage("/Error");
        return new RedirectResult(System.Web.HttpUtility.UrlDecode(continueWith));
    }
    
    public async Task<IActionResult> OnPostRejectAsync()
    {
        //todo: redirect to /Error with message access_denied
        return RedirectToPage("/Error?message=access_denied");
    }
    
}

public class PermitInputModel
{
    public Guid ClientId { get; init; }
    public List<string> Scopes { get; init; }
    public string Continue { get; init; }
}