namespace WebApi.Domain.Models
{
    public record UpdateEventRequestModel
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
