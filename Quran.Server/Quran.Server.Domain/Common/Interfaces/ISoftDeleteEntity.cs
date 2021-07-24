using System;

namespace Quran.Server.Domain.Common
{
    public interface ISoftDeleteEntity
    {
        DateTime? Deleted { get; set; }
        bool IsDeleted { get; set; }
        Guid? DeletedBy { get; set; }
    }

    
}