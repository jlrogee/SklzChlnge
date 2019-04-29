using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.ApiResults;
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
        public async Task<ActionResult> Get()
        {
            if (!HttpContext.Request.Cookies.TryGetValue("userId", out var userId))
                return Ok();
            
            var links = await _linkService.GetLinks(userId);
            return Ok(
                links.Select(l => 
                    new GetAllLinksResult
                    {
                        ShortLink = l.ShortLink,
                        OriginalLink = l.OriginalLink,
                        HitCount = l.HitCount
                    }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var link = await _linkService.GetLinkById(id);
            if (link == null)
                return NotFound();

            await _linkService.AddHitToLink(link);

            //return Redirect(link.OriginalLink);
            return Ok(new GetLinkResult {Link = link.OriginalLink});
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AddLinkModel model)
        {
            if (!Uri.TryCreate(model.Url, UriKind.Absolute, out _))
                return BadRequest();

            if (!HttpContext.Request.Cookies.TryGetValue("userId", out var userId))
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

            await _linkService.CreateLink(newLink);

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