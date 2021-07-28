using AutoMapper;
using Quran.Server.Application.Common.Mappings;
using Quran.Server.Domain.Entities;
using Quran.Server.Domain.Enums;

namespace Quran.Server.Application.Quran
{
    public class SurahDto : IMapFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public PlaceOfRevelationType PlaceOfRevelationType { get; set; }
        public int RevelationSequenceNo { get; set; }
        public int AyatCount { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Surah, SurahDto>()
                .ForMember(x=>x.AyatCount,y => y.MapFrom(x=>x.Ayat.Count));
        }
    }
}