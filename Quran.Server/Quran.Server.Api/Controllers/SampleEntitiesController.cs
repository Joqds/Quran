using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quran.Server.Application.Common.Models;
using Quran.Server.Application.SampleEntities.Commands.CreateSampleEntity;
using Quran.Server.Application.SampleEntities.Commands.DeleteSampleEntity;
using Quran.Server.Application.SampleEntities.Commands.UpdateSampleEntity;
using Quran.Server.Application.SampleEntities.Queries.GetSampleEntitiesWithPagination;

namespace Quran.Server.Api.Controllers
{
    [Authorize]
    public class SampleEntitiesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<SampleEntityDto>>> GetSampleEntitiesWithPagination([FromQuery] GetSampleEntitiesWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateSampleEntityCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateSampleEntityCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

//        [HttpPut("[action]")]
//        public async Task<ActionResult> UpdateItemDetails(int id, UpdateSampleEntityDetailCommand command)
//        {
//            if (id != command.Id)
//            {
//                return BadRequest();
//            }
//
//            await Mediator.Send(command);
//
//            return NoContent();
//        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteSampleEntityCommand { Id = id });

            return NoContent();
        }
    }
}
