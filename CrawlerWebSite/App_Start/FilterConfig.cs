using System.Web;
using System.Web.Mvc;
using WebMarkupMin.AspNet4.Mvc;

namespace CrawlerWebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            filters.Add(new MinifyHtmlAttribute());
            //filters.Add(new MinifyXhtmlAttribute());
            filters.Add(new MinifyXmlAttribute());
            filters.Add(new CompressContentAttribute());
        }
    }
}
