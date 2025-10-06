using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Abstractions
{
    public interface IAppDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
