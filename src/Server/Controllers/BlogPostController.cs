using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using blog.Shared;

namespace blog.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostController: ControllerBase
    {
        [HttpGet]
        public Post GetPostById()
        {
            return new Post();
        }
        
        [HttpGet]
        public Post GetPostBySlug()
        {
            return new Post();
        }
        
        [HttpGet]
        public IEnumerable<Post> GetPostsByTag()
        {
            return new List<Post>();
        }
        
        [HttpGet]
        public IEnumerable<Post> GetAllPosts()
        {
            return new List<Post>();
        }
    }
}