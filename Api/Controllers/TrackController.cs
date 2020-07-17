using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public TrackController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<TrackController>
        [HttpGet]
        public IActionResult Get([FromBody] TrackSearch search,
            [FromServices] IGetTracksQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<TrackController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id,
            [FromServices] IGetOneTrackQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<TrackController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] TrackDto dto,
            [FromServices] ICreateTrackCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // PUT api/<TrackController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TrackController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
