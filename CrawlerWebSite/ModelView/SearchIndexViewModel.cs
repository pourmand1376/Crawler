using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CrawlerWebSite.Models;

namespace CrawlerWebSite.ModelView
{
    public class SearchIndexViewModel
    {
        public List<SearchResult> SearchResults { get; set; }
        [Required(ErrorMessage = "متن جستجو نمیتواند خالی باشد.")]
        [MinLength(3,ErrorMessage = "متن جستجو میبایست حداقل 3 حرف داشته باشد")]
        public string Expression { get; set; }
        public int PageNo { get; set; }
        public int PageCount { get; set; }
    }
}