using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Technical.Assessment.Api.Dto;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RespondentController : ControllerBase
    {
        /// <summary>
        /// service's instance
        /// </summary>
        private readonly IRespondentService service;

        /// <summary>
        /// Imapper's instance
        /// </summary>
        private readonly IMapper _Mapper;

        /// <summary>
        /// Constructor's method
        /// </summary>
        /// <param name="service"></param>
        public RespondentController(IRespondentService service, IMapper _Mapper)
        {
            this.service = service;
            this._Mapper = _Mapper;
        }

        /// <summary>
        /// Create an user
        /// </summary>
        /// <param name="respondentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RespondentDto respondentDto)
        {
            var entity = _Mapper.Map<Respondent>(respondentDto);
            await service.InsertAsync(entity);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] RespondentDto respondentDto)
        {
            var entity = _Mapper.Map<Respondent>(respondentDto);
            await service.UpdateAsync(entity);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Ok(await service.GetAllAsync(null,null,null,false));
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(string email)
        {
            await service.DeleteAsync(email);
            return Ok();
        }
    }
}
