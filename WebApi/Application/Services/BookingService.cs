using OneOf;
using OneOf.Types;
using WebApi.Domain.Models;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;

namespace WebApi.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public async Task<OneOf<Success, Error<string>, Error>> BookEventSeatsAsync(BookEventSeatsRequestModel bookEventSeatsRequestModel, CancellationToken cancellationToken)
        {
            if (bookEventSeatsRequestModel.ApplicationId == Guid.Empty)
            {
                return new Error<string>("ApplicationId is required.");
            }

            if (bookEventSeatsRequestModel.UserId == Guid.Empty)
            {
                return new Error<string>("UserId is required.");
            }

            if (bookEventSeatsRequestModel.EventId == Guid.Empty)
            {
                return new Error<string>("EventId is required.");
            }

            if (bookEventSeatsRequestModel.TicketQuantity < 1 || bookEventSeatsRequestModel.TicketQuantity > 10)
            {
                return new Error<string>("TicketQuantity should be between 1 and 10.");
            }

            return await bookingRepository.BookEventSeatsAsync(bookEventSeatsRequestModel, cancellationToken);
        }
    }
}
