using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Technical.Assessment.Api.Dto;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SurveyController : ControllerBase
    {
        /// <summary>
        /// service's instance
        /// </summary>
        private readonly ISurveyService service;

        /// <summary>
        /// Imapper's instance
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// Constructor's method
        /// </summary>
        /// <param name="service"></param>
        public SurveyController(ISurveyService service, IMapper _Mapper)
        {
            this.service = service;
            this._Mapper = _Mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SurveyDto surveyDto)
        {
            var entity = _Mapper.Map<Survey>(surveyDto);
            await service.InsertAsync(entity);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SurveyDto surveyDto)
        {
            var entity = _Mapper.Map<Survey>(surveyDto);
            entity.Id = id;
            await service.UpdateAsync(entity);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await service.GetAllAsync(null, null, null, false));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }
    }
}
