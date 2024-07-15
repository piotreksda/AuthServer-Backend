// using MediatR;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using OAuth2OpenId.Application.Commands.Clients.CreateClient;
// using OAuth2OpenId.Application.Queries.GetClientDetail;
// using OAuth2OpenId.Application.Queries.GetClientList;
// using OAuth2OpenIdPresentation.Extensions;
//
// namespace OAuth2OpenIdPresentation.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class AppsController : Controller
// {
//     private readonly IMediator _mediator;
//
//     public AppsController(IMediator mediator)
//     {
//         _mediator = mediator;
//     }
//
//     // [HttpGet()]
//     // [Authorize]
//     // public async Task<IActionResult> GetAppsList()
//     // {
//     //     var userId = HttpContext.GetUserId();
//     //     if (userId.IsFailure)
//     //     {
//     //         throw new Exception();
//     //     }
//     //
//     //     GetClientListQuery query = new GetClientListQuery(userId.Value!);
//     //     
//     //     var model = await _mediator.Send(query);
//     //
//     //     return View(model);
//     // }
//
//     [HttpGet("{clientId:guid}")]
//     [Authorize]
//     public async Task<IActionResult> GetAppDetail([FromRoute] Guid clientId)
//     {
//         var userId = HttpContext.GetUserId();
//         if (userId.IsFailure)
//         {
//             throw new Exception();
//         }
//
//         GetClientDetailQuery query = new GetClientDetailQuery(userId.Value!, clientId);
//
//         var model = await _mediator.Send(query);
//
//         return View(model);
//     }
//
//     [HttpPost]
//     [Authorize]
//     public async Task CreateApp([FromBody] CreateClientInputModel model)
//     {
//         var userId = HttpContext.GetUserId();
//         if (userId.IsFailure)
//         {
//             throw new Exception();
//         }
//
//         CreateClientCommand command = new CreateClientCommand(userId.Value!, model);
//
//         await _mediator.Send(command);
//     }
// }