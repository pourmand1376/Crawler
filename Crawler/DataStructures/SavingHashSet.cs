using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Crawler
{
    class SavingHashSet:SaveDataStructures
    {
        private HashSet<string> _crawledUrls;
        public SavingHashSet():base("HashSet")
        {
            _crawledUrls = new HashSet<string>();
            Print.Show("Reading hashset...");
            ReadInitialHashSet();
        }

        private void ReadInitialHashSet()
        {
            var items = Collection.FindAll();
            var enumerable = items as Data[] ?? items.ToArray();
            Print.Show("Hashset count " + enumerable.Count());
            if (enumerable.Any())
            {
                _crawledUrls = new HashSet<string>(enumerable.Select(t=>t.Content));
            }
        }

        public bool Contains(string item) => _crawledUrls.Contains(item);
        
        public bool Add(string item)
        {
            Collection.Insert(new Data(item));
            return _crawledUrls.Add(item);
        }
    }
}
