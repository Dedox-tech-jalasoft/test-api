namespace WebApi.Infrastructure.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Event? Event { get; set; }
        public int Count { get; set; }
    }
}
