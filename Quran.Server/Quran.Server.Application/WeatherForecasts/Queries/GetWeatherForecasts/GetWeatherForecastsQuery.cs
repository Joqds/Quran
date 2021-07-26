using MediatR;
using System.Collections.Generic;

namespace Quran.Server.Application.WeatherForecasts.Queries.GetWeatherForecasts
{
    public class GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>
    {
    }
}
