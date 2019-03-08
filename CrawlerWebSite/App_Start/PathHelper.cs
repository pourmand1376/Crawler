using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrawlerWebSite
{
    public static class PathHelper
    {
        public static string ResolvePath(string path)
        {
            try
            {
               return System.Web.HttpContext.Current.Server.MapPath(path);

            }
            catch (Exception ex)
            {
                return System.Web.Hosting.HostingEnvironment.MapPath(path);
            }
        }
    }
}