using LiteDB;

namespace Indexer
{
    public class UrlSiteMapperRecord
    {
        [BsonId]
        //کد ذخیره شده سایت
        public int Id { get; set; }
        //ادرس
        [BsonField]
        public string Url { get; set; }
        //خلاصه یا چکیده سایت
        [BsonField]
        public string Abstract { get; set; }
        [BsonField]
        //موضوع
        public string Title { get; set; }
    }
}
