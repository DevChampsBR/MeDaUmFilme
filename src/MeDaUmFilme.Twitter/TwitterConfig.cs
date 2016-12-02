using System;

namespace MeDaUmFilme.Twitter
{
    public class TwitterConfig
    {
        public TwitterConfig(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
            Console.WriteLine($"Consumer key: {consumerKey}, cons sec: {consumerSecret}, access token: {accessToken}, acc t secret: {accessTokenSecret}");
        }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
