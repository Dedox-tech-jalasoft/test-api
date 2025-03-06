using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;

namespace WebApi.Domain.Repositories
{
    public interface IEventRepository
    {
        public Task<OneOf<List<EventModel>, Error>> GetAllEventsAsync(Guid applicationId, CancellationToken cancellationToken);
    }
}
