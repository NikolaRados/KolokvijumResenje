using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Queries;
using Application.Search;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public InvoiceController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public IActionResult Get([FromBody] InvoiceSearch search,
            [FromServices] IGetInvoiceQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }
    }
}
