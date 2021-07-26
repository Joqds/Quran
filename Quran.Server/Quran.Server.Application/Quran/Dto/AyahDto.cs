using AutoMapper;
using Quran.Server.Application.Common.Mappings;
using Quran.Server.Domain.Entities;
using Quran.Server.Domain.Enums;

namespace Quran.Server.Application.Quran
{
    public class AyahDto:IMapFrom
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SurahId { get; set; }
        public string SurahName { get; set; }
        public int RubId { get; set; }
        public int AyahInSurah { get; set; }
        public int RubJoz { get; set; }
        public int RubRubInJoz { get; set; }
        public int PageId { get; set; }
        public SajdahType SajdahType { get; set; }
        public string SajdahReason { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ayah, AyahDto>();
        }
    }
}