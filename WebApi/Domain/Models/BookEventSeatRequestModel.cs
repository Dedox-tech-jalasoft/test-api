namespace WebApi.Domain.Models
{
    public record BookEventSeatsRequestModel
    {
        public Guid ApplicationId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public int TicketQuantity { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }
}
