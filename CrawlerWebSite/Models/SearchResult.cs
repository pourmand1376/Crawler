using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrawlerWebSite.Models
{
    public class SearchResult
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double Rank { get; set; }
    }
}