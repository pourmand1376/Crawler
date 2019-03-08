using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Newtonsoft.Json;

namespace Crawler
{
    class SavingQueue: SaveDataStructures
    {
        private Queue<string> _queue;
        
        public SavingQueue():base("Queue")
        {
            _queue = new Queue<string>();
            Print.Show("Initializing Queue");
            ReadInitialQueue();
        }
        public int Count => _queue.Count;
        public string Dequeue() => _queue.Dequeue();
        private void ReadInitialQueue()
        {
            var queue= Collection.FindAll();
            Print.Show("Queue Item count "+queue.Count() );
            var enumerable = queue as Data[] ?? queue.ToArray();
            if(enumerable.Any())
                _queue = new Queue<string>(enumerable.Select(t=>t.Content));
        }

        public void Enqueue(string item)
        {
            _queue.Enqueue(item);
            Collection.Insert(new Data(item));
        }

    }
}
