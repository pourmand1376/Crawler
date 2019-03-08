using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer
{
    class Program
    {
        static void Main(string[] args)
        {
            IPrint print = new Print();
            Indexer index = new Indexer(print);
            index.Run();
            print.Show("Indexing finished... Be happy");
            Console.ReadLine();
        }
    }
}
