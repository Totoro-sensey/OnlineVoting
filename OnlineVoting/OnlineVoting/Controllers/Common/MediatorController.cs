using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineVoting.Controllers.Common
{
    public abstract class MediatorController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
