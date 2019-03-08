using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler
{
    class HtmlParser
    {
        

        public HtmlParser()
        {
            
        }
        public IEnumerable<string> Parse(HtmlDocument html,Uri url)
        {
            Print.Show("Parsing url:"+url.AbsoluteUri);
            var linkedPages = html.DocumentNode.Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => !String.IsNullOrEmpty(u));
            Print.Show(linkedPages.Count()+" urls in"+url.AbsoluteUri);

            List<string> uriList = new List<string>();
            foreach (string linkedPage in linkedPages)
            {
                if(linkedPage.Contains("#"))continue;
                var result = ValidateUrl.Validate(linkedPage);
                try
                {
                    if (!result.Item1)
                    {
                        uriList.Add(new Uri(url, linkedPage).AbsoluteUri);
                    }
                    else uriList.Add(linkedPage);
                }
                catch (Exception ex)
                {
                    Print.Show(ex.Message);
                }

            }
            return uriList;
        }

        
    }
}
