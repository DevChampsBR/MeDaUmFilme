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

        public Task ListenAsync(string searchTerm, Action<string> received)
        {
            var stream = Stream.CreateFilteredStream();
            stream.AddTrack(searchTerm);
            stream.MatchingTweetReceived += (sender, args) =>
            {
                var fullText = args.Tweet.FullText;
                var mentions = args.Tweet.UserMentions;
                var removedCount = 0;
                var lastIndex = 0;
                foreach (var mention in mentions)
                {
                    for (int i = 0; i < mention.Indices.Count; i += 2)
                    {
                        var firstPart = fullText.Substring(lastIndex - removedCount, mention.Indices[i] - removedCount);
                        var secondPart = fullText.Substring(mention.Indices[i + 1] - removedCount);
                        fullText = firstPart + secondPart;
                        lastIndex = mention.Indices[i + 1];
                        removedCount += mention.Indices[i + 1] - mention.Indices[i];
                    }
                }
                received(fullText);
            };
            return stream.StartStreamMatchingAllConditionsAsync();
        }
    }
}