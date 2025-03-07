using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IBookingService
    {
        public Task<OneOf<Success, Error<string>, Error>> BookEventSeatsAsync(BookEventSeatsRequestModel bookEventSeatsRequestModel, CancellationToken cancellationToken);
    }
}
