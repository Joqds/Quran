using System;
using System.Collections.Generic;

namespace Quran.Server.Domain.Common
{
    public interface IHasDomainEvent<T>
    {
        List<DomainEvent<T>> DomainEvents { get; set; }
    }

    public abstract class DomainEvent<T>
    {
        protected DomainEvent(T item)
        {
            Item = item;
        }

        public bool IsPublished { get; set; }
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
        public T Item { get; }
    }
}
