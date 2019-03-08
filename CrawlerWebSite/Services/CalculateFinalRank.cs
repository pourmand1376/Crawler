using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrawlerWebSite.Models;

namespace CrawlerWebSite.Services
{
    public class CalculateFinalRank
    {
        //ضریب تایتل
        private const double TitleConstant = 1;
        private const double bodyConstant=1;
        public Dictionary<int,double> CalculateRank(Dictionary<int,double> title,
            Dictionary<int,double> body)
        {
            Dictionary<int,double> final = new Dictionary<int, double>();

            foreach (KeyValuePair<int, double> keyValuePair in body)
            {
                final.Add(keyValuePair.Key,bodyConstant*keyValuePair.Value);
            }

            foreach (KeyValuePair<int, double> keyValuePair in title)
            {
                if (final.ContainsKey(keyValuePair.Key))
                {
                    double previous = final[keyValuePair.Key];
                    final[keyValuePair.Key] = previous + 
                                              TitleConstant * keyValuePair.Value;
                }
                else
                {
                    final.Add(keyValuePair.Key,keyValuePair.Value*TitleConstant);
                }
            }
            

            return final;

        }
    }
}