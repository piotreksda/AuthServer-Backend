using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OAuth2OpenId.Application.Abstractions;
using OAuth2OpenId.Domain.Entities;
using OAuth2OpenId.Domain.Models;
using OAuth2OpenId.Domain.ValueObjects;

namespace OAuth2OpenId.Application.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ActionResult>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ActionResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsUsernameAvailableAsync(request.Model.UserName))
        {
            return new BadRequestObjectResult("Username already taken.");
        }
        
        Password pwd = Password.CreatePassword(request.Model.Password);
        
        var user = new User(request.Model.UserName, "", pwd, false);
        
        await _userRepository.AddAsync(user, cancellationToken);

        await _userRepository.SaveChangesAsync(cancellationToken);
        
        return new OkResult();
    }
}