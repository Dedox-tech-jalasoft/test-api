using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IEventService
    {
        public Task<OneOf<List<EventModel>, Error<string>, Error>> GetAllEventsAsync(Guid applicationId, CancellationToken cancellationToken);
        public Task<OneOf<EventDetailsModel, Error<string>, NotFound, Error>> GetEventAsync(Guid applicationId, Guid eventId, CancellationToken cancellationToken);
        public Task<OneOf<List<EventModel>, Error<string>, NotFound, Error>> GetEventsByUserIdAsync(Guid applicationId, Guid userId, CancellationToken cancellationToken);
        public Task<OneOf<Success<Guid>, Error<string>, Error>> CreateEventAsync(CreateEventRequestModel createEventRequestModel, CancellationToken cancellationToken);
    }
}
