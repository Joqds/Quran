using Microsoft.EntityFrameworkCore;

using Quran.Server.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;

namespace Quran.Server.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Ayah> Ayat { get; set; }
        DbSet<Surah> Sovar { get; set; }
        DbSet<Rub> Arba { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
