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

        public async Task<OneOf<Success<Guid>, Error<string>, Error>> CreateEventAsync(CreateEventRequestModel createEventRequestModel, CancellationToken cancellationToken)
        {
            if (createEventRequestModel.ApplicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (createEventRequestModel.Price <= 0)
            {
                return new Error<string>("Price should be higher than 0.");
            }

            return await eventRepository.CreateEventAsync(createEventRequestModel, cancellationToken);
        }

        public async Task<OneOf<Success, Error<string>, NotFound, Error>> DeleteEventAsync(Guid applicationId, Guid eventId, CancellationToken cancellationToken)
        {
            if (applicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (eventId == Guid.Empty)
            {
                return new Error<string>("EventId is required.");
            }

            OneOf<Success, NotFound, Error> deleteResult = await eventRepository
                .DeleteEventAsync(applicationId, eventId, cancellationToken);

            return deleteResult.Match<OneOf<Success, Error<string>, NotFound, Error>>(
                success => success,
                notFound => notFound,
                error => error);
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

        public async Task<OneOf<List<EventModel>, Error<string>, NotFound, Error>> GetEventsByUserIdAsync(Guid applicationId, Guid userId, CancellationToken cancellationToken)
        {
            if (applicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (userId == Guid.Empty)
            {
                return new Error<string>("UserId is required.");
            }

            OneOf<List<EventModel>, NotFound, Error> events = await eventRepository
                .GetEventsByUserIdAsync(applicationId, userId, cancellationToken);

            return events.Match<OneOf<List<EventModel>, Error<string>, NotFound, Error>>(
                events => events,
                notFound => notFound,
                error => error);
        }

        public async Task<OneOf<Success, Error<string>, NotFound, Error>> UpdateEventAsync(Guid applicationId, Guid eventId, UpdateEventRequestModel updateEventRequestModel, CancellationToken cancellationToken)
        {
            if (applicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (eventId == Guid.Empty)
            {
                return new Error<string>("EventId is required.");
            }

            OneOf<Success, NotFound, Error> updateResult = await eventRepository
                .UpdateEventAsync(applicationId, eventId, updateEventRequestModel, cancellationToken);

            return updateResult.Match<OneOf<Success, Error<string>, NotFound, Error>>(
                success => success,
                notFound => notFound,
                error => error);
        }
    }
}
