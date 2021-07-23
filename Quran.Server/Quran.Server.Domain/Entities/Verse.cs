using System.Collections.Generic;
using Quran.Server.Domain.Common;
using Quran.Server.Domain.Events;

namespace Quran.Server.Domain.Entities
{
    public class SampleEntity:IHasDomainEvent
    {
        //todo: implement

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value && _done == false)
                {
                    DomainEvents.Add(new SampleEvent(this));
                }

                _done = value;
            }
        }

        public List<DomainEvent> DomainEvents { get; set; }
    }
}
