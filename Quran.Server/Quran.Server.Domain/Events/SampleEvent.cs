using Quran.Server.Domain.Common;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Domain.Events
{
    public class SampleEvent:DomainEvent<SampleEntity>
    {
        public SampleEvent(SampleEntity item) : base(item)
        {
        }
    }
}
