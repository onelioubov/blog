using System;
using Contentful.Core;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Shared;
using Microsoft.Extensions.Logging;

namespace blog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BlogPostsController: ControllerBase
    {
        private readonly ILogger<BlogPostsController> _logger;
        private readonly ContentfulClient _contentClient;
        public BlogPostsController(ILogger<BlogPostsController> logger, ContentfulClient contentClient)
        {
            _logger = logger;
            _contentClient = contentClient;
        }
        
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Post>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all blog posts.");
            try
            {
                var blogPosts = await _contentClient.GetEntries<Post>();
                return Ok(blogPosts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve blogPosts.");
                return BadRequest(e);
            }
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