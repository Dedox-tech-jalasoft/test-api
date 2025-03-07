namespace WebApi.Domain.Models
{
    public record EventDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int AvailableTickets { get; set; }
        public decimal Price { get; set; }
    }
}
