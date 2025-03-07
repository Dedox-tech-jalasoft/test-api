using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Context;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger<BookingRepository> logger;

        public BookingRepository(DatabaseContext databaseContext, ILogger<BookingRepository> logger)
        {
            this.databaseContext = databaseContext;
            this.logger = logger;
        }

        public async Task<OneOf<Success, Error<string>, Error>> BookEventSeatsAsync(BookEventSeatsRequestModel bookEventSeatsRequestModel, CancellationToken cancellationToken)
        {
            try
            {
                bool doesEventExist = await databaseContext.Events
                    .Where(entity => entity.ApplicationId ==  bookEventSeatsRequestModel.ApplicationId)
                    .AnyAsync(entity => entity.Id == bookEventSeatsRequestModel.EventId, cancellationToken);

                if (!doesEventExist)
                {
                    return new Error<string>($"Event {bookEventSeatsRequestModel.EventId} does not exist.");
                }

                bool doesUserExist = await databaseContext.Users
                    .AnyAsync(entity => entity.Id == bookEventSeatsRequestModel.UserId, cancellationToken);

                if (!doesUserExist)
                {
                    return new Error<string>($"User {bookEventSeatsRequestModel.UserId} does not exist.");
                }

                Booking? bookingEntity = await databaseContext.Bookings
                    .Where(entity =>
                        entity.EventId == bookEventSeatsRequestModel.EventId &&
                        entity.UserId == bookEventSeatsRequestModel.UserId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (bookingEntity is null)
                {
                    Booking newBookingEntity = new()
                    {
                        EventId = bookEventSeatsRequestModel.EventId,
                        UserId = bookEventSeatsRequestModel.UserId,
                        Count = bookEventSeatsRequestModel.TicketQuantity
                    };

                    await databaseContext.Bookings.AddAsync(newBookingEntity, cancellationToken);

                    await databaseContext.SaveChangesAsync(cancellationToken);

                    return new Success();
                }

                else
                {
                    bookingEntity.Count += bookEventSeatsRequestModel.TicketQuantity;

                    await databaseContext.SaveChangesAsync(cancellationToken);

                    return new Success();
                }
            }

            catch (Exception ex) {

                logger.LogError(ex, "Error booking seats for Event {EventId} with User {UserId}", bookEventSeatsRequestModel.EventId, bookEventSeatsRequestModel.UserId);

                return new Error();
            }
        }
    }
}
