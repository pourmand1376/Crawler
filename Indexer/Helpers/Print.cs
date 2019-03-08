using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer
{
   public class Print:IPrint
    {
        public void Show(string data)
        {
            Console.WriteLine(data);
        }
    }
}
