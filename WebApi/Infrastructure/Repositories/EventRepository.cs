using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Context;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger<EventRepository> logger;

        public EventRepository(DatabaseContext databaseContext, ILogger<EventRepository> logger)
        {
            this.databaseContext = databaseContext;
            this.logger = logger;
        }

        public async Task<OneOf<List<EventModel>, Error>> GetAllEventsAsync(Guid applicationId, CancellationToken cancellationToken)
        {
            try
            {
                List<EventModel> events = await databaseContext.Events
                    .AsNoTracking()
                    .TagWithCallSite()
                    .Where(eventEntity => eventEntity.ApplicationId == applicationId)
                    .Select(eventEntity => new EventModel()
                    {
                        Id = eventEntity.Id,
                        Name = eventEntity.Name,
                        Date = eventEntity.Date,
                        Price = eventEntity.Price,
                        Image = eventEntity.Image,
                    })
                    .ToListAsync(cancellationToken);

                return events;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting the list of events");

                return new Error();
            }
        }

        public async Task<OneOf<EventDetailsModel, NotFound, Error>> GetEventAsync(Guid applicationId, Guid eventId, CancellationToken cancellationToken)
        {
            try
            {
                EventDetailsModel? eventDetails = await databaseContext.Events
                    .AsNoTracking()
                    .TagWithCallSite()
                    .Where(eventEntity => eventEntity.ApplicationId == applicationId)
                    .Where(evenEntity => evenEntity.Id == eventId)
                    .Select(evenEntity => new EventDetailsModel()
                    {
                        Id = evenEntity.Id,
                        Name = evenEntity.Name,
                        Date = evenEntity.Date,
                        Price = evenEntity.Price,
                        Description = evenEntity.Description,
                        Image = evenEntity.Image
                    })
                    .FirstOrDefaultAsync(cancellationToken);

                if (eventDetails is null)
                {
                    return new NotFound();
                }

                return eventDetails;
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting event {EventId} from database", eventId);

                return new Error();
            }
        }

        public async Task<OneOf<List<EventModel>, NotFound, Error>> GetEventsByUserIdAsync(Guid applicationId, Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                User? userEntity = await databaseContext.Users
                    .AsNoTracking()
                    .TagWithCallSite()
                    .Include(userEntity => userEntity.Events)
                    .Where(userEntity => userEntity.Id == userId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (userEntity is null)
                {
                    return new NotFound();
                }

                return userEntity.Events
                    .Where(eventEntity => eventEntity.ApplicationId == applicationId)
                    .Select(evenEntity => new EventModel()
                    {
                        Id = evenEntity.Id,
                        Name = evenEntity.Name,
                        Date = evenEntity.Date,
                        Price = evenEntity.Price,
                        Image = evenEntity.Image
                    })
                    .ToList();
            }

            catch (Exception ex)
            {
                logger.LogError(ex, "Error getting the list of events for user: {UserId}", userId);

                return new Error();
            }
        }
    }
}
