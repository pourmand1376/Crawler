
using System;

namespace Crawler
{
    internal class Crawl
    {
        private static string[] Seeds;
        private static SavingQueue Queue;
        private static HtmlParser Parser;
        private static SavingHashSet CrawledUrls;
        
        public Crawl()
        {

            if (CrawledUrls == null)
            {
                CrawledUrls = new SavingHashSet();
            }

            if (Parser == null)
            {
                Parser = new HtmlParser();
            }

            if (Queue == null)
            {
                Queue = new SavingQueue();
            }

            if (Seeds == null)
            {
                Seeds = new[]
            {
                //"http://peyvandha.ir/",
                "http://www.safheha.ir/",
                "http://gowebsite.ir/",
                "http://1000site.ir",
                "https://khabarfarsi.com/farsi-news-site",
                "http://gadgetnews.net",
                "https://webcaster.ir",
                "https://pafcoerp.com",
                "https://www.alexa.com/topsites/countries/IR"
            };
            }
        }

        public void Start()
        {
            Print.Show("Start crawling...");
            if (Queue.Count == 0)
            {
                foreach (var seed in Seeds)
                {
                    DownloadAndEnqueue(seed);
                }
            }
            //Now Retrieving from Queue
            GetFromQueue();
        }

        public void DownloadAndEnqueue(string seed)
        {
            //validate
            try
            {
                var validate = ValidateUrl.Validate(seed);
                if (!validate.Item1)
                {
                    return;
                }

                //if is in hashset
                if (CrawledUrls.Contains(validate.Item2.AbsoluteUri))
                {
                    return;
                }

                CrawledUrls.Add(validate.Item2.AbsoluteUri);

                //download and save html file
                var html = new HtmlDownloader(validate.Item2).Load();
                if (html == null)
                {
                    return;
                }

                //parse html file to get urls
                var linkedPages = Parser.Parse(html, validate.Item2);
                foreach (string linkedPage in linkedPages)
                {
                    Queue.Enqueue(linkedPage);
                }
            }
            catch (Exception ex) { }

        }
        private void GetFromQueue()
        {
            while (Queue.Count > 0)
            {
                try
                {
                    DownloadAndEnqueue(Queue.Dequeue());
                }
                catch (Exception ex) { }
            }
        }
    }
}
