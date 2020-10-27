using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using Tweetinvi;
using Tweetinvi.Models;
using System.IO;
using Tweetinvi.Parameters;
using Tweetinvi.Parameters.V2;
using Tweetinvi.Models.V2;

namespace CSharpTracker
{
    class Twitter
    {
        private string _appToken;
        private string _appTokenSecret;
        private string _consumerKey;
        private string _consumerKeySecret;
        private string _bearerToken;
        private TwitterClient twitterClient;


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

        public string ConsumerKeySecret
        {
            get
            {
                return _consumerKeySecret;
            }
            set
            {
                _consumerKeySecret = value;
            }
        }

        public Twitter()
        {
            _appToken = ConfigurationManager.AppSettings["TwitterAppToken"];
            _appTokenSecret = ConfigurationManager.AppSettings["TwitterAppTokenSecret"];
            _bearerToken = ConfigurationManager.AppSettings["TwitterBearerToken"];

            Debug.WriteLine(_appToken);
            Debug.WriteLine(_appTokenSecret);

            twitterClient = new TwitterClient(_consumerKey, _consumerKeySecret, _appToken, _appTokenSecret);
        }

        #region Authentication
        public async void AuthenticationRequest()
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            var authenticationRequest = await twitterClient.Auth.RequestAuthenticationUrlAsync();

            Process.Start(new ProcessStartInfo(authenticationRequest.AuthorizationURL)
            {
                UseShellExecute = true
            });
        }

        public async void AuthenticationReceive(string pinCode, IAuthenticationRequest authenticationRequest)
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            var userCredentials = await twitterClient.Auth.RequestCredentialsFromVerifierCodeAsync(pinCode, authenticationRequest);
        }

        private async Task<bool> CreateUserClient(ITwitterCredentials userCredentials)
        {
            twitterClient = new TwitterClient(userCredentials);
            var user = await twitterClient.Users.GetAuthenticatedUserAsync();

            ConsumerKey = userCredentials.AccessToken;
            ConsumerKeySecret = userCredentials.AccessTokenSecret;

            if (twitterClient == null)
                return false;

            return true;
        }

        private bool CheckTwitterClient()
        {
            if (twitterClient == null)
                return false;

            if (String.IsNullOrEmpty(ConsumerKey) || String.IsNullOrEmpty(ConsumerKeySecret))
                return false;

            return true;
        }

        #endregion

        #region Publish Tweet

        public async Task<long> PostTweet(string body)
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            if (body.Length > 240)
                throw new ArgumentOutOfRangeException("Tweet should be in 240 characters only");

            var tweet = await twitterClient.Tweets.PublishTweetAsync(body);

            return tweet.Id;
        }

        public async Task<long> PostTweet(string body, string filePath)
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            if (body.Length > 240)
                throw new ArgumentOutOfRangeException("Tweet should be in 240 characters only");

            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("FilePath is empty");

            var tweetinviLogoBinary = File.ReadAllBytes(filePath);
            var uploadedImage = await twitterClient.Upload.UploadTweetImageAsync(tweetinviLogoBinary);
            var tweet = await twitterClient.Tweets.PublishTweetAsync(new PublishTweetParameters("uhmm")
            {
                Medias = { uploadedImage }
            });

            return tweet.Id;
        }

        public async Task<long> PostTweet(string body, string filePath, DateTime schedule)
        {
            return 0;
        }

        #endregion

        #region Retrieve Tweet

        public async Task<Tweet> GetTweet(string tweetId)
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            var publishedTweet = await twitterClient.Tweets.GetTweetAsync(1312077189051940866);

            var tweetResponse = await twitterClient.TweetsV2.GetTweetAsync(new GetTweetV2Parameters(1312077189051940866)
            {
                MediaFields =
                {
                    TweetResponseFields.Media.OrganicMetrics,
                    TweetResponseFields.Media.PublicMetrics,
                    TweetResponseFields.Media.NonPublicMetrics
                },
                Expansions =
                {
                    TweetResponseFields.Expansions.AuthorId,
                    TweetResponseFields.Expansions.ReferencedTweetsId,
                    TweetResponseFields.Expansions.AttachmentsMediaKeys,
                    TweetResponseFields.Expansions.EntitiesMentionsUsername
                },
                TweetFields =
                {
                    TweetResponseFields.Tweet.Attachments,
                    TweetResponseFields.Tweet.Entities,
                    TweetResponseFields.Tweet.CreatedAt,
                    TweetResponseFields.Tweet.Text
                },
                UserFields = TweetResponseFields.User.ALL
            });

            return new Tweet()
            {
                UserID = 0,
                Username = tweetResponse.Tweet.AuthorId,
                TweetID = tweetResponse.Tweet.Id,
                Body = tweetResponse.Tweet.Text,
                ImpressionCount = tweetResponse.Tweet.OrganicMetrics.ImpressionCount,
                ProfileClickCount = tweetResponse.Tweet.OrganicMetrics.UserProfileClicks,
                LikeCount = tweetResponse.Tweet.OrganicMetrics.LikeCount,
                RetweetCount = tweetResponse.Tweet.OrganicMetrics.RetweetCount,
                Posted = true,
                Schedule = null
            };
        }

        #endregion
    }
}
