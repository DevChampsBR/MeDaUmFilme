using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeDaUmFilme.Language;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace MeDaUmFilme
{
    public class Program
    {
        private static IAnalyzer languageAnalyzer;
        private static IMeDaUmFilmeSearch meDaUmFilmeSearch;

        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("TWITTER_ENV") ?? "Development";
            var cwd = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(cwd)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLanguage(configuration.GetSection("Language"));
            serviceCollection.AddTwitter(configuration.GetSection("Twitter"));
            serviceCollection.AddMeDaUmFilme();
            var services = serviceCollection.BuildServiceProvider();
            languageAnalyzer = services.GetService<IAnalyzer>();
            meDaUmFilmeSearch = services.GetService<IMeDaUmFilmeSearch>();
            var twitter = services.GetService<Twitter.Twitter>();
            twitter.ListenAsync("@medaumfilme", Listen);
            Console.WriteLine("Waiting...");
            Console.ReadLine();
        }

        private async static void Listen(string sanitizedText, ITweet tweet)
        {
            try
            {
                Console.WriteLine($"Searched: {sanitizedText}");
                var intent = await languageAnalyzer.AnalyzeAsync(sanitizedText);
                if (intent.Name == "BuscaTitulo")
                {
                    var omdbRequest = new OmbdRequest()
                    {
                        Title = intent.Entities["Titulo"]
                    };
                    var movie = await meDaUmFilmeSearch.GetMovie(omdbRequest);
                    var replyText = $"Found: {movie.Title} from {movie.Year}";
                    Console.WriteLine(replyText);
                    ReplyToTweet(tweet, replyText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unmanaged error:\n" + ex.ToString());
            }
        }

        private static void ReplyToTweet(ITweet tweet, string text)
        {
            var response = Tweetinvi.Tweet.PublishTweet($"@{tweet.CreatedBy.ScreenName} {text}",
                new PublishTweetOptionalParameters()
                {
                    InReplyToTweet = tweet
                }
            );
        }
    }
}