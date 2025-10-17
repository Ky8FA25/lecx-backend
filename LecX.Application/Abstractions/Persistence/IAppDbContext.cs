using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Abstractions.Persistence
{
    public interface IAppDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
