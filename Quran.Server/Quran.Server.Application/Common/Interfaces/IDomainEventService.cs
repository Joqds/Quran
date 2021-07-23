using System.Threading.Tasks;
using Quran.Server.Domain.Common;

namespace Quran.Server.Application.Common.Interfaces
{
    public interface IDomainEventService<T>
    {
        Task Publish(DomainEvent<T> domainEvent);
    }
}
