using MediatR;
using Quran.Server.Domain.Common;

namespace Quran.Server.Application.Common.Models
{
    public class DomainEventNotification<TDomainEvent,TEventModel> : INotification where TDomainEvent : DomainEvent<TEventModel>
    {
        public DomainEventNotification(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }

        public TDomainEvent DomainEvent { get; }
    }
}
