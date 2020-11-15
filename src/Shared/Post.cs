using System;

namespace blog.Shared
{
    public class Post
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Title { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }
    }
}