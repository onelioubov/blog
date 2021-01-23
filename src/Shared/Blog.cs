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
        
        // TODO: Figure out why Slugify is ignoring "." despite it being omitted by default.
        // Slugify.Core repo: https://github.com/ctolkien/Slugify
        // The slug generator doesn't seem to be omitting the "." character, despite it being part of the default
        // config. Manually replacing the "." with an empty string for now.
        public string Slug => _slugGenerator.GenerateSlug(Title.Replace(".", String.Empty));
        
        public string Title { get; set; }

        public string Description { get; set; }
        
        public string Content { get; set; }
            
        public List<string> Tags { get; set; }
            
        public SysData Sys { get; set; }
            
        public Image TitleImage { get; set; }
    }
    
    public class Image
    {
        public string Url { get; set; }
    }

    public class SysData
    {
        public DateTime FirstPublishedAt { get; set; }
    }
}