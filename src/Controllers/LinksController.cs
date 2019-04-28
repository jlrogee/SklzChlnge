using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] {"link"};
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            return "link";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}