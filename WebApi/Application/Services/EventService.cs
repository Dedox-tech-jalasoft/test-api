using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;

namespace WebApi.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<OneOf<IEnumerable<EventModel>, Error>> GetAllEventsAsync(CancellationToken cancellationToken)
        {
            return await eventRepository.GetAllEventsAsync(cancellationToken);
        }
    }
}
