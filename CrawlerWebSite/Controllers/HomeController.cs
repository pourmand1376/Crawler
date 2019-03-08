using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrawlerWebSite.Models;
using CrawlerWebSite.ModelView;
using CrawlerWebSite.Services;
using Indexer;

namespace CrawlerWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchIndexViewModel searchIndexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HtmlWordParser parser = new HtmlWordParser();
            string[] searchedWords = parser.ParseToList(searchIndexViewModel.Expression);
            
            CalculateIndexRank calcBody = new CalculateIndexRank(MvcApplication.BodyIndex);
            CalculateIndexRank calcTitle = new CalculateIndexRank(MvcApplication.TitleIndex);

            var titleBestResults =calcTitle.GetBestResults(searchedWords);
            var bodyBestResults = calcBody.GetBestResults(searchedWords);

            CalculateFinalRank calculateFinalRank = new CalculateFinalRank();
            var results = calculateFinalRank.CalculateRank(titleBestResults,bodyBestResults);

            List<SearchResult> searchResults = new List<SearchResult>();
            foreach (KeyValuePair<int, double> keyValuePair in results)
            {
                var siteinfo = MvcApplication.UrlSiteMapper[keyValuePair.Key];
                searchResults.Add(new SearchResult()
                {
                    Title = siteinfo.Title,
                    Description = siteinfo.Abstract,
                    Url = siteinfo.Url,
                    Rank = keyValuePair.Value,
                });
            }
            List<SearchResult> SortedList = searchResults.OrderByDescending(o => o.Rank).ToList();

            stopwatch.Stop();

            ViewBag.Search = true;
            ViewBag.ElapsedTime = stopwatch.Elapsed.Milliseconds;
            ViewBag.SearchCount = SortedList.Count;

            const int pagecount = 20;
            previous = searchIndexViewModel.Expression;
            return View("Index",new SearchIndexViewModel
            {
                SearchResults   = SortedList.Skip(searchIndexViewModel.PageNo*pagecount).Take(pagecount).ToList(),
                PageNo = searchIndexViewModel.PageNo,
                PageCount = SortedList.Count/pagecount,
                Expression = searchIndexViewModel.Expression
            });
        }

        public static string previous = "";
        public ActionResult PageNo(int id)
        {
            return Search(new SearchIndexViewModel()
            {
                Expression = previous,
                PageNo = id-1
            });
        }
    }
}