using System;

namespace Quran.Server.Domain.Common
{
    public interface IAuditEntity<T,TUserKey>: IBaseEntity<T>
    {
        DateTime Created { get; set; }

        TUserKey CreatedBy { get; set; }

        DateTime? LastModified { get; set; }

        TUserKey LastModifiedBy { get; set; }
    }
}
