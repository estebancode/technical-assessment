using AutoMapper;
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
    public class AuthenticationController : ControllerBase
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
        public AuthenticationController(IRespondentService service, IMapper _Mapper)
        {
            this.service = service;
            this._Mapper = _Mapper;
        }

        /// <summary>
        /// Authenticate an user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] UserDto userDto)
        {
            var entity = _Mapper.Map<Respondent>(userDto);
            return Ok(new { token = await service.AuthenticaUserAsync(entity) });
        }
    }
}
