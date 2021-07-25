using Quran.Server.Domain.Common;

using System.Threading.Tasks;

namespace Quran.Server.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
