using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Login;

public class LoginCommand : IRequest<ActionResult>
{
    public LoginCommand(LoginInputModel model)
    {
        Model = model;
    }

    public LoginInputModel Model { get; init; } 
}