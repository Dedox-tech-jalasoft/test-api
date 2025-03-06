using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Entities;

namespace WebApi.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
