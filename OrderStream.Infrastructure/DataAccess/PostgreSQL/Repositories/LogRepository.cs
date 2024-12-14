namespace OrderStream.Infrastructure.DataAccess.PostgreSQL;

using OrderStream.Infrastructure.DataAccess.PostgreSQL.Interfaces;
using OrderStream.Core.Entities;

public class LogRepository : ILogRepository
{
    private readonly AppDbContext _context;

    public LogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddLogAsync(Log log)
    {
        if (_context?.Logs == null)
        {
            throw new InvalidOperationException("Database context or Logs collection is not initialized.");
        }

        await _context.Logs.AddAsync(log);
        await _context.SaveChangesAsync();
    }
} 