﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MeDaUmFilme.Language;

namespace MeDaUmFilme.Api
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
            twitter.ListenAsync("@medaumfilme", Listen).Wait();
        }

        private async static void Listen(string tweet)
        {
            Console.WriteLine($"Searched: {tweet}");
            var intent = await languageAnalyzer.AnalyzeAsync(tweet);
            if (intent.Name == "BuscaTitulo")
            {
                var omdbRequest = new OmbdRequest()
                {
                    Title = intent.Entities["Titulo"]
                };
                var movie = await meDaUmFilmeSearch.GetMovie(omdbRequest);
                Console.WriteLine($"Found: {movie.Title} from {movie.Year}");
            }
        }
    }
}