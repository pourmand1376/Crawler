using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Crawler;
using LiteDB;

namespace Indexer
{
    public class UrlSiteMapper
    {
        private readonly IPrint _print;
        private LiteDatabase db;
        private LiteCollection<UrlSiteMapperRecord> liteCollection;
        private int id = 1;
        public UrlSiteMapper(IPrint print, string filename = "Sites.Db")
        {
            _print = print;
            db = new LiteDatabase(filename);
            liteCollection = db.GetCollection<UrlSiteMapperRecord>("SiteUrls");
        }
        public UrlSiteMapper(string filename):this(new Print(),filename) { }


        public Dictionary<int, UrlSiteMapperRecord> ReadInitDictionary()

        {
            var items = liteCollection.FindAll();
            var urlSiteMapperRecords = items as UrlSiteMapperRecord[] ?? items.ToArray();
            var dic = new Dictionary<int, UrlSiteMapperRecord>();
            if (urlSiteMapperRecords.Any())
            {
                foreach (UrlSiteMapperRecord indexRecordStructure in urlSiteMapperRecords)
                {
                    dic.Add(indexRecordStructure.Id,indexRecordStructure);
                }
            }
            return dic;
        }
        public int SaveSite(SiteInfo info)
        {
            id=liteCollection.Count()+1;
            liteCollection.Insert(new UrlSiteMapperRecord()
            {
                Url = info.Url,
                Abstract = BuildAbstract(info.BodyContent),
                Title = info.TitleContent,
                Id =id
            });
            return id;
        }

        private string BuildAbstract(string body)
        {
            string[] bodies = body.Split(' ');
            StringBuilder sb = new StringBuilder();
            int to = 20;
            if (bodies.Length < 20)
                to = bodies.Length;

            for (int i = 0; i < to; i++)
            {
                sb.Append(bodies[i]+" ");
            }

            return sb.ToString();
        }

    }
}
