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

        public async Task<OneOf<List<EventModel>, Error<string>, Error>> GetAllEventsAsync(Guid applicationId, CancellationToken cancellationToken)
        {  
            if (applicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            OneOf<List<EventModel>, Error> events = await eventRepository
                .GetAllEventsAsync(applicationId, cancellationToken);

            return events.Match<OneOf<List<EventModel>, Error<string>, Error>>(
                eventListResult => eventListResult,
                error => error);    
        }

        public async Task<OneOf<EventDetailsModel, Error<string>, NotFound, Error>> GetEventAsync(Guid applicationId, Guid eventId, CancellationToken cancellationToken)
        {
            if (applicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (eventId == Guid.Empty)
            {
                return new Error<string>("EventId is required.");
            }

            OneOf<EventDetailsModel, NotFound, Error> eventDetails = await eventRepository
                .GetEventAsync(applicationId, eventId, cancellationToken);

            return eventDetails.Match<OneOf<EventDetailsModel, Error<string>, NotFound, Error>>(
                eventDetails => eventDetails,
                notFound => notFound,
                error => error);
        }
    }
}
