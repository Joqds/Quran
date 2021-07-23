using System.Threading.Tasks;
using Quran.Server.Domain.Common;

namespace Quran.Server.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
