using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Context;

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
    }
}
