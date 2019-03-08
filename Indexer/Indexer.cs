using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Crawler;


namespace Indexer
{
    class Indexer
    {
        private CrawlerRetriever crawler;
        private DictionaryHandler bodyDictionary;
        private DictionaryHandler titleDictionary;
        private UrlSiteMapper urlMapper;
        private HtmlWordParser wordParser;
        private IPrint _print;

        public Indexer(IPrint print)
        {
            _print = print;
            crawler = new CrawlerRetriever(print);
            bodyDictionary = new DictionaryHandler("BodyIndex.Db",print);
            titleDictionary = new DictionaryHandler("TitleIndex.Db", print);
            urlMapper = new UrlSiteMapper(print);
            wordParser = new HtmlWordParser(print);
            
        }

        public void Run()
        {
            bodyDictionary.ReadInitHashSet();
            titleDictionary.ReadInitHashSet();
            while (crawler.HasItem())
            {
                //retrieve 
                SiteInfo siteinfo = crawler.Retrieve();

                //split site info by space
                if (siteinfo.TitleContent is null
                    || siteinfo.BodyContent is null)
                {
                    crawler.Delete(siteinfo);
                    continue;
                }
                var titles = wordParser.Parse(siteinfo.TitleContent);
                var bodies = wordParser.Parse(siteinfo.BodyContent);

                _print.Show("title count "+titles.Count);
                _print.Show("body count " +bodies.Count);

                //map url to id and save that data
                int urlId=urlMapper.SaveSite(siteinfo);
                //create item for each word
                foreach (string title in titles.Keys)
                {
                    titleDictionary.Add(title, new UrlWordCount()
                    {
                        Count = titles[title],
                        UrlId = urlId
                    });
                }

                foreach (string body in bodies.Keys)
                {
                    bodyDictionary.Add(body, new UrlWordCount()
                    {
                        Count = bodies[body],
                        UrlId = urlId,
                    });
                }

                //delete the item previously taken
                crawler.Delete(siteinfo);
            }
        }

        
    }
}
