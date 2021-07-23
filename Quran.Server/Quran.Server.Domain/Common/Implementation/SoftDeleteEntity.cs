using System;

namespace Quran.Server.Domain.Common
{
    public class SoftDeleteEntity<T, TUserKey> : BaseEntity<T>, ISoftDeleteEntity<T, TUserKey>
    {
        public DateTime? Deleted { get; set; }
        public TUserKey DeletedBy { get; set; }
    }
}