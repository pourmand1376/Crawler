using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Crawler;
using LiteDB;

namespace Indexer
{
    class CrawlerRetriever
    {
        private readonly IPrint _print;
        public LiteDatabase db;
        private LiteCollection<SiteInfo> crawled;

        public CrawlerRetriever(IPrint print)
        {
            _print = print;
            db = new LiteDatabase(@"Crawler.db");
            crawled = db.GetCollection<SiteInfo>("SiteInfo");
        }

        public SiteInfo Retrieve()
        {
            SiteInfo item=crawled.Find(p => true, 0, 1).FirstOrDefault();
            _print.Show("remaining:"+ crawled.Count());
            return item;
        }

        public bool Delete(SiteInfo item)
        {
            return crawled.Delete(item?.DatabaseId);
        }

        public bool HasItem()
        {
            return crawled.Count() > 0;
        }
    }

    
}
