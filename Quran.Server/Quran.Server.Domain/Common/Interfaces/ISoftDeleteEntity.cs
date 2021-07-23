using System;

namespace Quran.Server.Domain.Common
{
    public interface ISoftDeleteEntity<T, TUserKey> : IBaseEntity<T>
    {
        DateTime? Deleted { get; set; }
        TUserKey DeletedBy { get; set; }
    }

    
}