using System;
//using Contentful.Core;
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
        //private readonly ContentfulClient _contentClient;
        public BlogPostsController(GraphQLHttpClient client, ILogger<BlogPostsController> logger/*,  ContentfulClient contentClient*/)
        {
            _client = client;
            _logger = logger;
            // _contentClient = contentClient;
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
                // var blogPosts = await _contentClient.GetEntries<Post>();
                // return Ok(blogPosts);
                var heroRequest = new GraphQLRequest
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
                    var result = await _client.SendQueryAsync<PostsCollection>(heroRequest);
                    _logger.LogDebug(result.Data.PostCollection.Items.First().Title);
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
                var heroRequest = new GraphQLRequest
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
                var result = await _client.SendQueryAsync<PostsCollection>(heroRequest);
                //var blogPosts = await _contentClient.GetEntries<Blog>();
                var post = result.Data.PostCollection.Items.FirstOrDefault(x => x.Slug == slug);
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