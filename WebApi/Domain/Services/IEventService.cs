using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IEventService
    {
        public Task<OneOf<List<EventModel>, Error<string>, Error>> GetAllEventsAsync(Guid applicationId, CancellationToken cancellationToken);
    }
}
