using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Repositories
{
    public interface IBookingRepository
    {
        public Task<OneOf<Success, Error<string>, Error>> BookEventSeatsAsync(BookEventSeatsRequestModel bookEventSeatsRequestModel, CancellationToken cancellationToken);
    }
}
