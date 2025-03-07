namespace WebApi.Infrastructure.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int TotalTicketsAssigned { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public Application? Application { get; set; }
        public Guid ApplicationId { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
