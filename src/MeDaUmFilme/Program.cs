using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeDaUmFilme.Language;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using MeDaUmFilme.Twitter;

namespace MeDaUmFilme
{
    public class Program
    {
        private static IAnalyzer languageAnalyzer;
        private static IMeDaUmFilmeSearch meDaUmFilmeSearch;
        private static Twitter.Twitter twitter;

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
            twitter = services.GetService<Twitter.Twitter>();
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
                switch (intent.Name)
                {
                    case "BuscaTitulo":
                        {
                            var replyText = "Não entendi.";
                            if (!intent.Entities.ContainsKey("Titulo"))
                            {
                                Console.WriteLine(replyText);
                                twitter.ReplyToTweet(tweet, replyText);
                                break;
                            }
                            var omdbRequest = new OmbdRequest()
                            {
                                Title = intent.Entities["Titulo"]
                            };
                            var movie = await meDaUmFilmeSearch.GetMovie(omdbRequest);
                            replyText = $"Found: {movie.Title} from {movie.Year}";
                            Console.WriteLine(replyText);
                            twitter.ReplyToTweet(tweet, replyText);
                            break;
                        }
                    case "BuscaQualquer":
                        {
                            var replyText = "Não entendi.";
                            var omdbRequest = new OmbdRequest();
                            var movie = await meDaUmFilmeSearch.GetMovie(omdbRequest);
                            replyText = $"Found: {movie.Title} from {movie.Year}";
                            Console.WriteLine(replyText);
                            twitter.ReplyToTweet(tweet, replyText);
                            break;
                        }
                    case "None":
                        {
                            var replyText = "Não entendi.";
                            Console.WriteLine(replyText);
                            twitter.ReplyToTweet(tweet, replyText);
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unmanaged error:\n" + ex.ToString());
            }
        }

    }
}