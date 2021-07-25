using System;

namespace Quran.Server.Domain.Common
{
    public abstract class AuditEntity : IAuditEntity
    {
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid? LastModifiedBy { get; set; }
    }

    public abstract class SoftDeleteEntity : ISoftDeleteEntity
    {
        public DateTime? Deleted { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedBy { get; set; } = null;
    }

    public abstract class FullAuditEntity : IAuditEntity, ISoftDeleteEntity
    {
        public DateTime Created { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? Deleted { get; set; } = null;
        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedBy { get; set; } = null;
    }
}