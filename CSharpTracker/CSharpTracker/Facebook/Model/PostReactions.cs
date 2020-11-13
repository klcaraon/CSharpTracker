using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTracker.Facebook.Model
{
    public class PostReactions
    {

        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
        public class Value2
        {
            public int like { get; set; }
            public int love { get; set; }
            public int wow { get; set; }
        }

        public class Value
        {
            public Value2 value { get; set; }
        }

        public class Datum
        {
            public List<Value> values { get; set; }
            public string id { get; set; }
        }

        public class Paging
        {
            public string previous { get; set; }
            public string next { get; set; }
        }
    }
}
