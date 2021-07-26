using System.Collections.Generic;
using System.Linq;

namespace Quran.Server.Application.Quran
{
    public class AyatChunkDto 
    {
        //todo: i can add additional data later

        public List<AyahDto> Ayat { get; set; }
        public int? FromPage => Ayat?.Min(x => x.PageId);
        public int? ToPage => Ayat?.Max(x => x.PageId);
        
    }
}