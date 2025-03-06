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

        [ProducesResponseType<IEnumerable<EventModel>>((int)HttpStatusCode.OK)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAllEventsAsync (CancellationToken cancellationToken)
        {
            OneOf<IEnumerable<EventModel>, Error> result = await eventService.GetAllEventsAsync(cancellationToken);

            return result.Match<ActionResult>(
                events => Ok(events),
                error => Problem());
        }
    }
}
