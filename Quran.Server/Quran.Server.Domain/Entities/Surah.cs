using System.Collections.Generic;
using Quran.Server.Domain.Enums;

namespace Quran.Server.Domain.Entities
{
    public class Surah
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public PlaceOfRevelationType PlaceOfRevelationType { get; set; }
        public int RevelationSequenceNo { get; set; }

        public virtual ICollection<Ayah> Ayat { get; set; }
    }
}