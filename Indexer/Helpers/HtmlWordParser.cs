using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexer
{
    public class HtmlWordParser
    {
        private readonly IPrint _print;

        public HtmlWordParser(IPrint print)
        {
            _print = print;
        }
        public HtmlWordParser():this(new Print()) { }

        public IDictionary<string, int> Parse(string plaintext)
        {
            plaintext = StripPunctuation(plaintext);
            var words =plaintext.Split(' ');
            var wordsCounts = from word in words
                group word by word.ToLower()
                into g
                select new { Word = g.Key, Count = g.Count() };

            return wordsCounts.ToDictionary(wc => wc.Word, wc => wc.Count);
        }

        public string[] ParseToList(string plaintext)
        {
           return Parse(plaintext).Keys.ToArray();
        }
        private string StripPunctuation(string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if ((char.IsPunctuation(c)||char.IsSeparator(c)
                     ||IsEscapeChar(c))
                    &&c != ' ')continue;
                    sb.Append(c);
            }
            return sb.ToString();
        }

        private bool IsEscapeChar(char c)
        {
            return c == '\n' || c == '\r' || c == '\t' || c == '\a' || c == '\b'
                ||c== '\f' ||c=='\v';
        }
    }
}
