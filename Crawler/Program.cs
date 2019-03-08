using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {

        static void Main(string[] args)
        {
            const int noOfCrawlers = 8;
            Crawl c = new Crawl();
            for (int i = 0; i < noOfCrawlers; i++)
            {
                var i1 = i;
                Task.Run(() =>
                {
                    if (i1 == 0)
                        c.Start();
                    else new Crawl().Start();
                });
            }

            Console.ReadLine();
        }
    }
}
