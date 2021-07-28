using System.Collections.Generic;
using MediatR;

namespace Quran.Server.Application.Quran.Queries.GetSurahList
{
    public class GetSurahListQuery : IRequest<List<SurahDto>>
    {
    }
}