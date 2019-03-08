using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Indexer;

namespace CrawlerWebSite
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ReadData();
        }

        public static Dictionary<string, List<UrlWordCount>> BodyIndex;
        public static Dictionary<string, List<UrlWordCount>> TitleIndex;
        public static Dictionary<int, UrlSiteMapperRecord> UrlSiteMapper;
        protected void ReadData()
        {
            DictionaryHandler bodyindex = new DictionaryHandler(
                PathHelper.ResolvePath("~/App_Data/BodyIndex.Db"));
             BodyIndex = bodyindex.ReadInitHashSet();
            DictionaryHandler titleindex = new DictionaryHandler(
                PathHelper.ResolvePath("~/App_Data/TitleIndex.Db"));
            TitleIndex=titleindex.ReadInitHashSet();

            UrlSiteMapper urlSiteMapper = new UrlSiteMapper(
                PathHelper.ResolvePath("~/App_Data/Sites.Db"));
            UrlSiteMapper = urlSiteMapper.ReadInitDictionary();
        }
    }
}
