﻿using System;
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

namespace CSharpTracker
{
    class Twitter
    {
        private string _appToken;
        private string _appTokenSecret;
        private string _consumerKey;
        private string _consumerSecretKey;
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
            _bearerToken = ConfigurationManager.AppSettings["TwitterBearerToken"];

            Debug.WriteLine(_appToken);
            Debug.WriteLine(_appTokenSecret);

            twitterClient = new TwitterClient(_consumerKey, _consumerSecretKey, _appToken, _appTokenSecret);
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
            ConsumerSecretKey = userCredentials.AccessTokenSecret;

            if (twitterClient == null)
                return false;

            return true;
        }
        #endregion

        #region Publish Tweet
        public async Task<long> Tweet(string body)
        {
            if (twitterClient == null)
                throw new NullReferenceException("Twitter client is null");

            if (body.Length > 240)
                throw new ArgumentOutOfRangeException("Tweet should be in 240 characters only");

            var tweet = await twitterClient.Tweets.PublishTweetAsync(body);

            return tweet.Id;
        }

        public async Task<long> Tweet(string body, string filePath)
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
        #endregion
    }
}
