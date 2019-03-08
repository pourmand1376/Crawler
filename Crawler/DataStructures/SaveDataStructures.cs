using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Crawler
{
    abstract class SaveDataStructures
    {
        protected const string QueueFile = "Data.Db";
        protected LiteDatabase Db;
        protected LiteCollection<Data> Collection;
        //public int CrawlerNo { get; set; }
        protected SaveDataStructures(string name)
        {
            //CrawlerNo = no;
            Print.Show("Loading db "+ QueueFile);
            Db = new LiteDatabase(QueueFile);
            Print.Show("Loading collection "+name);
            Collection = Db.GetCollection<Data>(name);
        }
        
    }

    public class Data
    {
        [BsonId]
        public ObjectId DatabaseId { get; set; }
        public string Content { get; set; }

        public Data(string content)
        {
            Content = content;
        }

        public Data()
        {

        }
    }
}
