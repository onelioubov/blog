using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Shared;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.Extensions.Logging;

namespace blog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostsController: ControllerBase
    {
        private readonly GraphQLHttpClient _client;
        private readonly ILogger<BlogPostsController> _logger;
        
        public BlogPostsController(GraphQLHttpClient client, ILogger<BlogPostsController> logger)
        {
            _client = client;
            _logger = logger;
        }
        
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PostsCollection), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Retrieving all blog posts.");
            try
            {
                
                var request = new GraphQLRequest
                    {
                        Query = @"
                            query {
                                postCollection {
  	                                items {
  	                                    title
  	                                    description
                                        content
                                        tags
                                        titleImage {
                                            url
                                        }
                                        sys {
                                            firstPublishedAt
                                        }
  	                                }
	                            }
                            }"
                    };
                
                    var result = await _client.SendQueryAsync<PostsCollection>(request);
                    return Ok(result.Data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve blogPosts.");
                return BadRequest(e);
            }
        }

        [Route("{slug}", Name = nameof(GetBySlug))]
        [HttpGet]
        [ProducesResponseType(typeof(Blog), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            _logger.LogInformation($"Retrieving post with slug {slug}.");
            try
            {
                var request = new GraphQLRequest
                {
                    Query = @"
                            query {
                                postCollection {
  	                                items {
  	                                    title
  	                                    description
                                        content
                                        tags
                                        titleImage {
                                            url
                                        }
                                        sys {
                                            firstPublishedAt
                                        }
  	                                }
	                            }
                            }"
                };
                
                var result = await _client.SendQueryAsync<PostsCollection>(request);
                var post = result.Data.PostCollection.Items.FirstOrDefault(x => x.Slug == slug);
                
                if (post == null)
                {
                    _logger.LogDebug("No post with that slug found.");
                    return NoContent();
                }
                
                return Ok(post);
            }
            
            catch (Exception e)
            {
                _logger.LogError(e, "Could not retrieve blog posts.");
                return BadRequest(e);
            }
        }
    }
}