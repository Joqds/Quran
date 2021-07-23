using AutoMapper;

namespace Quran.Server.Application.Common.Mappings
{
    public interface IMapFrom
    {
        void Mapping(Profile profile); //=> profile.CreateMap(typeof(T), GetType());
    }
}
