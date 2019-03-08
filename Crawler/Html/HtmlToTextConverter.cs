using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Crawler.Html
{
    class HtmlToTextConverter
    {
        public HtmlToTextConverter()
        {
            
        }

        public string ToText(string htmlString)
        {
            htmlString= Regex.Replace(htmlString, @"<(.|n)*?>", " ",RegexOptions.Compiled)
                .Replace("&nbsp", " ");
            htmlString = Regex.Replace(htmlString, @"\s\s+"," ",RegexOptions.Compiled);
            return htmlString;
        }
    }
}
