using System;

namespace Quran.Server.Domain.Common
{
    public class AuditEntity<T, TUserKey>: BaseEntity<T>,IAuditEntity<T, TUserKey>
    {
        public DateTime Created { get; set; }
        public TUserKey CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public TUserKey LastModifiedBy { get; set; }
    }
}
