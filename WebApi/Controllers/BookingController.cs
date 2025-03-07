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
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> BookEventSeatsAsync([FromBody] BookEventSeatsRequestModel bookEventSeatsRequestModel, CancellationToken cancellationToken)
        {
            OneOf<Success, Error<string>, Error> result = await bookingService.BookEventSeatsAsync(bookEventSeatsRequestModel, cancellationToken);

            return result.Match<ActionResult>(
                created => StatusCode((int)HttpStatusCode.Created),
                badRequest => Problem(detail: badRequest.Value, statusCode: (int)HttpStatusCode.BadRequest),
                error => Problem());
        }
    }
}
