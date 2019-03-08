using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrawlerWebSite.Models;
using Indexer;

namespace CrawlerWebSite.Services
{
    public class CalculateIndexRank
    {
        private readonly Dictionary<string, List<UrlWordCount>> _dictionary;

        public CalculateIndexRank(Dictionary<string,List<UrlWordCount>> dictionary)
        {
            _dictionary = dictionary;
        }

        public Dictionary<int,double> GetBestResults(string[] searchedWords)
        {
            TfIdfCalc tfIdfCalc = new TfIdfCalc(_dictionary);
            
            Dictionary<int,double> siteRank = new Dictionary<int, double>();

            foreach (string t in searchedWords)
            {
                Dictionary<int, double> result=tfIdfCalc.GetAllUrlsWithRank(t);
                if(result==null) continue;
                foreach (KeyValuePair<int, double> keyValuePair in result)
                {
                    if (!siteRank.ContainsKey(keyValuePair.Key))
                    {
                        siteRank.Add(keyValuePair.Key,keyValuePair.Value);
                    }
                    else
                    {
                        double previous = siteRank[keyValuePair.Key];
                        siteRank[keyValuePair.Key] = previous + keyValuePair.Value;
                    }
                }
            }
            //نرمال سازی داده ها
            if(siteRank.Any())
            siteRank = NormalizeResult(siteRank);

            return siteRank;
        }

        private Dictionary<int, double> NormalizeResult(Dictionary<int, double> siteRank)
        {
            double max = siteRank.Values.Max();

            KeyValuePair<int, double>[] keyValuePairs = siteRank.ToArray();
            foreach (KeyValuePair<int, double> keyValuePair in keyValuePairs)
            {
                siteRank[keyValuePair.Key] = keyValuePair.Value / max;
            }

            return siteRank;
        }
    }
}