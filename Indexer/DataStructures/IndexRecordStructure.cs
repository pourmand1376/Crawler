using System.Collections.Generic;
using LiteDB;

namespace Indexer
{
    
        class IndexRecordStructure
        {
            [BsonId]
            public string Title { get; set; }
            [BsonField]
            public List<UrlWordCount> Indexes { get; set; }
            [BsonField]
            public long Count { get; set; }    
        }
    
}
