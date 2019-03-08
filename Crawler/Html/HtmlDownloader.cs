using System;
using System.Text;
using Crawler.Html;
using HtmlAgilityPack;

namespace Crawler
{
    class HtmlDownloader
    {
        public Uri Uri { get; set; }
        
        public static HtmlSaver Worker = new HtmlSaver();
        public HtmlDownloader(Uri uri)
        {
            //CrawlerNo = no;
            Uri = uri;
        }

        public HtmlDocument Load()
        {
            Print.Show("Loading url: " + Uri.AbsoluteUri);
            HtmlDocument doc=null;
            try
            {
                doc = new HtmlWeb().Load(Uri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Print.Show(ex.Message);
                return null; 
            }

            Print.Show("Saving url: " + Uri.AbsoluteUri+ " Size:"+doc.ParsedText.Length);
            if (doc.ParsedText == null || doc.ParsedText.Length < 10) return doc;
            
            //get text from title and body
            var title=doc.DocumentNode.SelectSingleNode("//head//title");
            var body =doc.DocumentNode.SelectSingleNode("//body");
            
            //remove script 
            var nodes = body.SelectNodes("//script|//style");
            foreach (var node in nodes)
                node.ParentNode.RemoveChild(node);
            
            HtmlToTextConverter textConverter = new HtmlToTextConverter();
            Worker.SaveDocument(new SiteInfo
            {
                BodyContent =textConverter.ToText(body.InnerText),
                TitleContent =textConverter.ToText(title.InnerText),
                Url =Uri.AbsoluteUri
            });
            return doc;
        }


        
    }
}
