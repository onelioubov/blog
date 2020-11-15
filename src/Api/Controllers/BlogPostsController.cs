using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Shared;

namespace blog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogPostsController: ControllerBase
    {
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            return Ok("");
        }
        
        [Route("{id:int}", Name = nameof(GetById))]
        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok("");
        }

        [Route("{slug}", Name = nameof(GetBySlug))]
        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            return Ok("");
        }
        
        [Route("{tag}", Name = nameof(GetByTag))]
        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByTag(string tag)
        {
            return Ok("");
        }
    }
}