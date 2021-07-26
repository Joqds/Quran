using System.Collections.Generic;
using System.Linq;

namespace Quran.Server.Application.Quran
{
    public class SurahChunkDto 
    {
        //todo: i can add additional data later

        public List<AyahDto> Ayat { get; set; }

        public SurahChunkDto(List<AyahDto> ayat, int surahFromPage, int surahToPage)
        {
            Ayat = ayat;
            SurahFromPage = surahFromPage;
            SurahToPage = surahToPage;
        }

        public int SurahFromPage { get; set; }
        public int SurahToPage { get; set; }
        public bool IsEndChunk => Ayat.Any(x => x.PageId == SurahToPage);
        public bool IsStartChunk => Ayat.Any(x => x.PageId == SurahToPage);
        public bool IsAllChunk => IsEndChunk && IsStartChunk;
        public List<int> CurrentChunkPages => Ayat.Select(x => x.PageId).Distinct().ToList();

    }
}