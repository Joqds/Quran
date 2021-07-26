using System;

namespace Quran.Server.Domain.Enums
{
    [Flags]
    public enum SajdahType
    {
        None=0,
        Place=1,
        Reason=2,
        PlaceAndReason=3
    }
}