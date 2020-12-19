using System;
using System.Collections.Generic;

namespace blog.Shared
{
    public class PostsCollection
    {
        public PostCollectionData PostCollection { get; set; }
    }
    public class PostCollectionData
    {
        public List<Blog> Items { get; set; }
    }
}