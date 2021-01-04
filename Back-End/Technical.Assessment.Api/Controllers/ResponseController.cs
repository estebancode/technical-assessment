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
    public class ResponseController : ControllerBase
    {
        /// <summary>
        /// service's instance
        /// </summary>
        private readonly IResponseService service;

        /// <summary>
        /// Imapper's instance
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// Constructor's method
        /// </summary>
        /// <param name="service"></param>
        public ResponseController(IResponseService service, IMapper _Mapper)
        {
            this.service = service;
            this._Mapper = _Mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResponseDto responseDto)
        {
            var entity = _Mapper.Map<Response>(responseDto);
            await service.InsertAsync(entity);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ResponseDto responseDto)
        {
            var entity = _Mapper.Map<Response>(responseDto);
            await service.UpdateAsync(entity);
            return Ok();
        }

        [HttpGet("GetGetAllBySurveyIdAndUser")]
        public IActionResult GetGetAllBySurveyIdAndUser(int surveyId,int respondentId)
        {
            return Ok(service.GetAllBySurveyIdAndUser(surveyId,respondentId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }
    }
}
