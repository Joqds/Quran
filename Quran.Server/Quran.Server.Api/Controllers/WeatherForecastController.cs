using Microsoft.AspNetCore.Mvc;

using Quran.Server.Application.WeatherForecasts.Queries.GetWeatherForecasts;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quran.Server.Api.Controllers
{
    public class WeatherForecastController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            return await Mediator.Send(new GetWeatherForecastsQuery());
        }
    }
}
