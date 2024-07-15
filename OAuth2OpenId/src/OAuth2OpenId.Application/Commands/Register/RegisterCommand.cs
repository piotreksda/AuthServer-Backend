using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Domain.Models;

namespace OAuth2OpenId.Application.Commands.Register;

public class RegisterCommand : IRequest<ActionResult>
{
    public RegisterCommand(RegisterInputModel model)
    {
        Model = model;
    }

    public RegisterInputModel Model { get; init; }
}