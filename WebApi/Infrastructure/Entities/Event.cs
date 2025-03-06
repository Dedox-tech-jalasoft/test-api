namespace WebApi.Infrastructure.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int AvailableTickets { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
