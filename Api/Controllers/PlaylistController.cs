using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public PlaylistController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // DELETE api/<PlaylistController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id,
            [FromServices] IDeletePlaylistCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
