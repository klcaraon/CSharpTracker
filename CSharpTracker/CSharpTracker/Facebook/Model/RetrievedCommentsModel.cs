using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTracker.Facebook.Model
{
    public class RetrievedCommentsModel
    {
        public Comments comments { get; set; }
        public string id { get; set; }
        public class Datum
        {
            public DateTime created_time { get; set; }
            public string message { get; set; }
            public string id { get; set; }
        }

        public class Cursors
        {
            public string before { get; set; }
            public string after { get; set; }
        }

        public class Paging
        {
            public Cursors cursors { get; set; }
        }

        public class Comments
        {
            public List<Datum> data { get; set; }
            public Paging paging { get; set; }
        }

    }
}
