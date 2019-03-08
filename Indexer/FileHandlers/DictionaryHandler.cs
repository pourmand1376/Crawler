using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteDB;

namespace Indexer
{
     public class DictionaryHandler
    {
        private readonly IPrint _print;
        private Dictionary<string, List<UrlWordCount>> index;
        private LiteDatabase db;
        private LiteCollection<IndexRecordStructure> DataBaseForIndex;
        public DictionaryHandler(string fileName, IPrint print)
        {
            _print = print;
            db = new LiteDatabase(fileName);
            DataBaseForIndex = db.GetCollection<IndexRecordStructure>("Indexes");
            index = new Dictionary<string, List<UrlWordCount>>();
        }

        public DictionaryHandler(string filename):this(filename,new Print())
        {
            
        }
        public Dictionary<string,List<UrlWordCount>> ReadInitHashSet()
        {
            var items = DataBaseForIndex.FindAll();
            //var enumerable = items as IndexRecordStructure[] ?? items.ToArray();
            //_print.Show("Hashset dictionary count " + enumerable.Length);
            if (items.Any())
            {
                index = new Dictionary<string, List<UrlWordCount>>();
                try
                {
                    foreach (var item in items)
                    {

                        IndexRecordStructure indexRecordStructure = item as IndexRecordStructure;
                        if (indexRecordStructure != null)
                        {
                            if (index.ContainsKey(indexRecordStructure.Title))
                            {
                                index[indexRecordStructure.Title] = indexRecordStructure.Indexes;
                            }
                            else
                                index.Add(indexRecordStructure.Title, indexRecordStructure.Indexes);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return index;
        }

        public void Add(string title, UrlWordCount urlIndex)
        {
            if (title.Length < 3)
            {
                //_print.Show("word is too small...");
                return;
            }

            if (title.Length > 20)
            {
                title = title.Substring(0, 20);
            }
            //if there isn't any list for this item
            if (!index.ContainsKey(title))
            {
                //_print.Show("added to dictionary");
                index.Add(title,new List<UrlWordCount>());
            }

            List<UrlWordCount> values = index[title];
            values.Add(urlIndex);

            var value=DataBaseForIndex.FindById(title);
            if (value == null)
                DataBaseForIndex.Insert(new IndexRecordStructure()
                {
                    Title = title,
                    Indexes = index[title],
                    Count = urlIndex.Count
                });
            else
            {

                DataBaseForIndex.Update(title, new IndexRecordStructure()
                {
                    Title = title,
                    Indexes = index[title],
                     Count = value.Count+urlIndex.Count
                });
            }
        }
    }
}
