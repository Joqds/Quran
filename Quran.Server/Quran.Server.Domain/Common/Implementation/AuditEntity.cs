using System;

namespace Quran.Server.Domain.Common
{
    public class AuditEntity: IAuditEntity
    {
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }
}