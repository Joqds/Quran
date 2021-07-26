using System.Collections.Generic;
using Quran.Server.Domain.Enums;

namespace Quran.Server.Domain.Entities
{
    public class Ayah
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SurahId { get; set; }
        public int RubId { get; set; }
        public int AyahInSurah { get; set; }
        public int AyahInRub { get; set; }
        public int PageId { get; set; }
        public int WordCount { get; set; }
        public int LetterCount { get; set; }
        public SajdahType SajdahType { get; set; }
        public string SajdahReason { get; set; }

        public virtual Rub Rub { get; set; }
        public virtual Surah Surah { get; set; }

        public virtual ICollection<Rub> FirstRub { get; set; }
        public virtual ICollection<Rub> LastRub { get; set; }
    }
}
