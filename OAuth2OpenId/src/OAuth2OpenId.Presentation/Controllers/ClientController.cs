using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Queries.GetClientList;

namespace OAuth2OpenId.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<object> GetClients()
    {
        GetClientListQuery query = new GetClientListQuery(1);

        return await _mediator.Send(query);
    }
    
    [HttpGet("{clientId:guid}")]
    public async Task<object> GetClient(Guid clientId)
    {
        return new();
    }
    
    [HttpPost]
    public async Task CreateClient()
    {
        return;
    }
    
    [HttpGet("{clientId:guid}/scope")]
    public async Task<object> GetClientScopes(Guid clientId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/scope/{scopeId:int}")]
    public async Task<object> GetClientScope(Guid clientId, int scopeId)
    {
        return new();
    }
    
    [HttpPost("{clientId:guid}/scope")]
    public async Task<object> CreateClientScope(Guid clientId)
    {
        return new();
    }
    
    [HttpDelete("{clientId:guid}/scope/{scopeId:int}")]
    public async Task<object> DeleteClientScope(Guid clientId, int scopeId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/grandType")]
    public async Task<object> GetClientGrandType(Guid clientId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/grandType/{grandTypeId:int}")]
    public async Task<object> GetClientGrandType(Guid clientId, int grandTypeId)
    {
        return new();
    }
    
    [HttpPost("{clientId:guid}/grandType")]
    public async Task<object> CreateClientGrandType(Guid clientId)
    {
        return new();
    }
    
    [HttpDelete("{clientId:guid}/grandType/{grandTypeId:int}")]
    public async Task<object> DeleteClientGrandType(Guid clientId, int grandTypeId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/environment")]
    public async Task<object> GetClientEnvironments(Guid clientId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/environment/{environmentId:int}")]
    public async Task<object> GetClientEnvironment(Guid clientId, int environmentId)
    {
        return new();
    }
    
    [HttpPost("{clientId:guid}/environment")]
    public async Task<object> CreateClientEnvironment()
    {
        return new();
    }
    
    [HttpDelete("{clientId:guid}/environment/{environmentId:int}")]
    public async Task<object> DeleteClientEnvironment(Guid clientId, int environmentId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/environment/{environmentId:int}/redirectUri")]
    public async Task<object> GetClientEnvironmentRedirectUris(Guid clientId, int environmentId)
    {
        return new();
    }
    
    [HttpGet("{clientId:guid}/environment/{environmentId:int}/redirectUri/{redirectUriId}")]
    public async Task<object> GetClientEnvironmentRedirectUri(Guid clientId, int environmentId, int redirectUriId)
    {
        return new();
    }
    
    [HttpPost("{clientId:guid}/environment/{environmentId:int}/redirectUri/{redirectUriId}")]
    public async Task<object> CreateClientEnvironmentRedirectUri()
    {
        return new();
    }
    
    [HttpDelete("{clientId:guid}/environment/{environmentId:int}/redirectUri/{redirectUriId}")]
    public async Task<object> DeleteClientEnvironmentRedirectUri(Guid clientId, int environmentId)
    {
        return new();
    }
    
    
}