//using System.Collections.Generic;
//
//using Quran.Server.Domain.Common;
//using Quran.Server.Domain.Events;
//
//namespace Quran.Server.Domain.Entities
//{
//    public class SampleEntity : FullAuditEntity, IHasDomainEvent
//    {
//        
//        public int Id { get; set; }
//        public string SampleProperty { get; set; }
//        private bool _done;
//        public bool Done
//        {
//            get => _done;
//            set
//            {
//                if (value && _done == false)
//                {
//                    DomainEvents.Add(new SampleEvent(this));
//                }
//
//                _done = value;
//            }
//        }
//
//        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
//    }
//}