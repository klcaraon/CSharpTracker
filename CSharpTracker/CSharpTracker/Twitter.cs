using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace CSharpTracker
{
    class Twitter
    {
        private string _appToken;
        private string _appTokenSecret;
        private string _consumerKey;
        private string _consumerSecretKey;

        public string ConsumerKey
        {
            get
            {
                return _consumerKey;
            }
            set
            {
                _consumerKey = value;
            }
        }

        public string ConsumerSecretKey
        {
            get
            {
                return _consumerSecretKey;
            }
            set
            {
                _consumerSecretKey = value;
            }
        }

        public Twitter()
        {
            _appToken = ConfigurationManager.AppSettings["TwitterAppToken"];
            _appTokenSecret = ConfigurationManager.AppSettings["TwitterAppTokenSecret"];

            Debug.WriteLine(_appToken);
            Debug.WriteLine(_appTokenSecret);
        }
    }
}
