using DevOps_Labs_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace DevOps_Labs_Backend.Context;

public interface IApplicationDbContext
{
    DbSet<Counter> Counters { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
