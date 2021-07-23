using System;

namespace Quran.Server.Domain.Common
{
    public interface IAuditEntity
    {
        DateTime Created { get; set; }

        Guid? CreatedBy { get; set; }

        DateTime? LastModified { get; set; }

        Guid? LastModifiedBy { get; set; }
    }
}
