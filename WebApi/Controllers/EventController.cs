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
    public class EventController : ControllerBase
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [ProducesResponseType<List<EventModel>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync ([FromQuery] Guid applicationId, CancellationToken cancellationToken)
        {
            OneOf<List<EventModel>, Error<string>, Error> result = await eventService.GetAllEventsAsync(applicationId, cancellationToken);

            return result.Match<ActionResult>(
                events => Ok(events),
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                error => Problem());
        }
    }
}
