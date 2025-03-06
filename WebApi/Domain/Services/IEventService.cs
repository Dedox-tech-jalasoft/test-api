using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Services
{
    public interface IEventService
    {
        public Task<OneOf<IEnumerable<EventModel>, Error>> GetAllEventsAsync(CancellationToken cancellationToken);
    }
}
