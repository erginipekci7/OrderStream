using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderStream.Core.Entities;

namespace OrderStream.Infrastructure.DataAccess.PostgreSQL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Log>? Logs { get; set; }
}