//using MediatR;
//
//using Microsoft.Extensions.Logging;
//
//using Quran.Server.Application.Common.Models;
//using Quran.Server.Domain.Events;
//
//using System.Threading;
//using System.Threading.Tasks;
//
//namespace Quran.Server.Application.SampleEntities.EventHandlers
//{
//    public class SampleEntityCompletedEventHandler : INotificationHandler<DomainEventNotification<SampleEvent>>
//    {
//        private readonly ILogger<SampleEntityCompletedEventHandler> _logger;
//
//        public SampleEntityCompletedEventHandler(ILogger<SampleEntityCompletedEventHandler> logger)
//        {
//            _logger = logger;
//        }
//
//        public Task Handle(DomainEventNotification<SampleEvent> notification, CancellationToken cancellationToken)
//        {
//            var domainEvent = notification.DomainEvent;
//
//            _logger.LogInformation("Joqds.Quran Domain Event: {DomainEvent}", domainEvent.GetType().Name);
//
//            return Task.CompletedTask;
//        }
//    }
//}
