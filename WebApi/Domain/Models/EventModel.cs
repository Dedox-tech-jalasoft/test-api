namespace WebApi.Domain.Models
{
    public record EventModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
