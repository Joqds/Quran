using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<SampleEntity> SampleEntities { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
