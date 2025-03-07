using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IEventService eventService;

        public UserController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [ProducesResponseType<List<EventModel>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{userId}/Events")]
        public async Task<IActionResult> GetEventsByUserIdAsync([FromQuery] Guid applicationId, [FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            OneOf<List<EventModel>, Error<string>, NotFound, Error> result = await eventService
                .GetEventsByUserIdAsync(applicationId, userId, cancellationToken);

            return result.Match<ActionResult>(
                events => Ok(events),
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                notFound => NotFound(),
                error => Problem());
        }
    }
}
