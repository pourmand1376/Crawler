using LiteDB;

namespace Crawler
{
  public  class SiteInfo
    {
        [BsonId]
        public ObjectId DatabaseId { get; set; }
        public string Url { get; set; }
        public string BodyContent { get; set; }
        public string TitleContent { get; set; }
    }
}
