using System;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;

namespace MeDaUmFilme.Twitter
{
    public class Twitter
    {
        private TwitterCredentials creds;

        public Twitter(TwitterConfig config)
        {
            creds = new TwitterCredentials(config.ConsumerKey, config.ConsumerSecret, config.AccessToken, config.AccessTokenSecret);
            Auth.SetUserCredentials(config.ConsumerKey, config.ConsumerSecret, config.AccessToken, config.AccessTokenSecret);
        }

        public Task Listen(string searchTerm, Action<string> received)
        {
            var stream = Stream.CreateFilteredStream();
            stream.AddTrack(searchTerm);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                received(args.Tweet.FullText);
            };
            return stream.StartStreamMatchingAllConditionsAsync();
        }
    }
}