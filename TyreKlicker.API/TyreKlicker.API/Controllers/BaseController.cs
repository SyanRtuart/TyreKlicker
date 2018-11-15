using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace TyreKlicker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        protected string CurrentUser => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}