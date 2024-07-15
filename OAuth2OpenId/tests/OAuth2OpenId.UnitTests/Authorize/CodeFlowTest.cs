using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Application.Commands.Authorize;
using OAuth2OpenId.Domain.Entities;

public class AuthorizeCommandHandlerTests
{
    private readonly Mock<IClientRepository> _authClientRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private AuthorizeCommandHandler _handler;

    public AuthorizeCommandHandlerTests()
    {
        _authClientRepositoryMock = new Mock<IClientRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
    }

    [Fact]
    public async Task Handle_UserIsNull_ShouldRedirectToLogin()
    {
        // // Arrange
        // var command = new AuthorizeCommand(
        // new AuthorizationRequest
        //     {
        //         client_id = Guid.NewGuid(),
        //         redirect_uri = "https://example.com/callback",
        //         response_type = "code",
        //         scope = "openid profile"
        //     }
        // );
        // _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext());
        //
        // _handler = new AuthorizeCommandHandler(
        //     _authClientRepositoryMock.Object,
        //     _httpContextAccessorMock.Object,
        //     _userRepositoryMock.Object);
        //
        // // Act
        // var result = await _handler.Handle(command, CancellationToken.None);
        //
        // // Assert
        // var redirectResult = Assert.IsType<RedirectResult>(result);
        // Assert.StartsWith("/Login?continue=", redirectResult.Url);
    }

    [Fact]
    public async Task Handle_ClientIsNull_ShouldRedirectOnError()
    {
        // // Arrange
        // var command = new AuthorizeCommand(new AuthorizationRequest
        // {
        //     client_id = Guid.NewGuid(),
        //     redirect_uri = "https://example.com/callback",
        //     response_type = "code",
        //     scope = "openid profile"
        // });
        //
        // var user = new User("test","test",true) { Id = 1 };
        // Client? client = null;
        //
        // _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(CreateHttpContext(user.Id));
        // _userRepositoryMock.Setup(x => x.GetByIdAsync(user.Id, false, default)).ReturnsAsync(user);
        // _authClientRepositoryMock.Setup(x => x.GetByIdAsync(command.Request.client_id, false, default)).ReturnsAsync(client);
        //
        // _handler = new AuthorizeCommandHandler(
        //     _authClientRepositoryMock.Object,
        //     _httpContextAccessorMock.Object,
        //     _userRepositoryMock.Object);
        //
        // // Act
        // var result = await _handler.Handle(command, CancellationToken.None);
        //
        // // Assert
        // var redirectResult = Assert.IsType<RedirectResult>(result);
        // Assert.Contains("Error", redirectResult.Url);
        // Assert.Contains("callback", redirectResult.Url);
    }

    // [Fact]
    // public async Task Handle_InvalidRequest_ResponseType_
    private HttpContext CreateHttpContext(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };
        var identity = new ClaimsIdentity(claims, "TestAuth");
        var user = new ClaimsPrincipal(identity);

        var context = new DefaultHttpContext
        {
            User = user,
        };
        
        context.Request.Scheme = "https";
        context.Request.Host = new HostString("example.com");
        context.Request.Path = "/authorize";

        return context;
    }
}
