using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Indexer;

namespace CrawlerWebSite.Services
{
    public class TfIdfCalc
    {
        private readonly Dictionary<string, List<UrlWordCount>> _dictionary;
        

        public TfIdfCalc(Dictionary<string, List<UrlWordCount>> dictionary)
        {
            _dictionary = dictionary;
        }

        public Dictionary<int,double> GetAllUrlsWithRank(string searchedWord)
        {
            //اگر کلا هیچ سایتی این کلمه را نداشت که هیچ
            if (!_dictionary.ContainsKey(searchedWord)) return null;

            var sites = _dictionary[searchedWord];

            double idf = CalulateIdf(sites);

            //sites with results 
            Dictionary<int,double> dictionary = new Dictionary<int, double>(); 

            foreach (UrlWordCount urlWordCount in sites)
            {
                dictionary.Add(urlWordCount.UrlId,
                    CalculateTfIdf(urlWordCount.Count,idf));
            }

            return dictionary;
        }

        private double CalculateTfIdf(int tf, double idf)
        {
            if (tf == 0) return 0;
            return (1 + Math.Log(tf)) * idf;
        }

        private double CalulateIdf(List<UrlWordCount> sites)
        {
            if (sites.Count == 0) return 0;
            return Math.Log(_dictionary.Count / sites.Count);
        }
    }
}