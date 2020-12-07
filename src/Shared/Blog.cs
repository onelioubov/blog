using System;
using System.Collections.Generic;
using Slugify;

namespace blog.Shared
{
    public class Blog
    {
        private readonly SlugHelper _slugGenerator;
        
        public Blog()
        {
            _slugGenerator = new SlugHelper();
        }
        
        public string Slug => _slugGenerator.GenerateSlug(Title);
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public string Content { get; set; }
            
        public List<string> Tags { get; set; }
            
        public SysData Sys { get; set; }
            
        public Image TitleImage { get; set; }
            
        public class Image
        {
            public string Url { get; set; }
        }

        public class SysData
        {
            public DateTime FirstPublishedAt { get; set; }
        }
    }
}