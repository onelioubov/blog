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
    [Route("[controller]")]
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

        [Route("{slug}", Name = nameof(GetBySlug))]
        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            _logger.LogInformation($"Retrieving post with slug {slug}.");
            try
            {
                var blogPosts = await _contentClient.GetEntries<Post>();
                var post = blogPosts.FirstOrDefault(x => x.Slug == slug);
                if (post == null)
                {
                    _logger.LogError("No post with that slug found.");
                    return NoContent();
                }
                else
                {
                    return Ok(post);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve blog post with that slug.");
                return BadRequest(e);
            }
        }
    }
}