using System.Collections.Generic;

namespace Quran.Server.Domain.Entities
{
    public class Rub
    {
        public int Id { get; set; }
        public int Joz { get; set; }
        public int RubInJoz { get; set; }
        public int FirstAyahId { get; set; }
        public int LastAyahId { get; set; }


        public virtual Ayah FirstAyah { get; set; }
        public virtual Ayah LastAyah { get; set; }
        
        public virtual ICollection<Ayah> Ayat { get; set; }
    }
}