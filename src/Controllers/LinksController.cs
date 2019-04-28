using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.BL;
using src.DAL;
using src.Models;

namespace src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinksService _linkService;
        private readonly IShortLinkGenerator _shortLinkGenerator;

        public LinksController(
            ILinksService linkService,
            IShortLinkGenerator shortLinkGenerator)
        {
            _linkService = linkService;
            _shortLinkGenerator = shortLinkGenerator;
        }
        
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
        public async Task<ActionResult> Post([FromBody] AddLinkModel model)
        {
            if (!Uri.TryCreate(model.Url, UriKind.Absolute, out _))
                return BadRequest();

            var userId = string.Empty;
            if(!HttpContext.Request.Cookies.TryGetValue("userId", out _))
            {
                userId = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies.Append("userId", userId);
            }

            var newLink = new Link
            {
                OriginalLink = model.Url,
                ShortLink = GenerateShortLink(),
                UserId = userId
            };

            return Ok(new
            {
                ShortLink = $"{Request.Host.Value}{Request.Path.Value}/{newLink.ShortLink}"
            });
        }

        private string GenerateShortLink()
        {
            return _shortLinkGenerator.GetShortLinkName(6);
        }
    }
}