namespace WebApi.Domain.Models
{
    public record CreateEventRequestModel
    {
        public Guid ApplicationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
