namespace WebApi.Infrastructure.Entities
{
    public class Application
    {
        public Guid Id { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
