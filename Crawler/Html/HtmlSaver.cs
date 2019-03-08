using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Crawler
{
    class HtmlSaver
    {
        
        public LiteDatabase db;
        private LiteCollection<SiteInfo> crawled;
        public HtmlSaver()
        {
            db = new LiteDatabase(@"Crawler.db");
            crawled=db.GetCollection<SiteInfo>("SiteInfo");
        }
        
        public void SaveDocument(SiteInfo info)
        {
            crawled.Insert(info);
        }
    }
}
