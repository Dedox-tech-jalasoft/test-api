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
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync ([FromQuery] Guid applicationId, CancellationToken cancellationToken)
        {
            OneOf<List<EventModel>, Error<string>, Error> result = await eventService
                .GetAllEventsAsync(applicationId, cancellationToken);

            return result.Match<ActionResult>(
                events => Ok(events),
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                error => Problem());
        }

        [ProducesResponseType<EventDetailsModel>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.NotFound)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("{eventId}")]
        public async Task<IActionResult> GetEventAsync([FromQuery] Guid applicationId, [FromRoute] Guid eventId, CancellationToken cancellationToken)
        {
            OneOf<EventDetailsModel, Error<string>, NotFound, Error> result = await eventService
                .GetEventAsync(applicationId, eventId, cancellationToken);

            return result.Match<ActionResult>(
                eventDetails => Ok(eventDetails),
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                notFound => NotFound(),
                error => Problem());
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<ActionResult> CreateEventAsync([FromBody] CreateEventRequestModel createEventRequestModel, CancellationToken cancellationToken)
        {
            OneOf<Success<Guid>, Error<string>, Error> result = await eventService
                .CreateEventAsync(createEventRequestModel, cancellationToken);

            return result.Match<ActionResult>(
                success => new ObjectResult(success.Value) { StatusCode = (int)HttpStatusCode.Created },
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                error => Problem());
        }
    }
}
