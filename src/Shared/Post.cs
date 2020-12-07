using System;
using System.Collections.Generic;
using Slugify;

// This is not currently used, but I'm keeping it around for a future thought.
namespace blog.Shared
{
    public class Post
    {
        private readonly SlugHelper _slugGenerator;
        public Post()
        {
            _slugGenerator = new SlugHelper();
        }

        public int Id { get; set; }
        
        public Uri TitleImageUrl { get; set; }

        public string Slug => _slugGenerator.GenerateSlug(Title);

        public string Title { get; set; }

        public DateTime PublishedAt { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
        
        public IEnumerable<string> Tags { get; set; }
    }
}