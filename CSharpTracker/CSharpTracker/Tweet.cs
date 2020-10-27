using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTracker
{
    class Tweet
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string TweetID { get; set; }
        public string Body { get; set; }
        public int ImpressionCount { get; set; }
        public int ProfileClickCount { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public bool Posted { get; set; }
        public DateTime? Schedule { get; set; }
    }
}
