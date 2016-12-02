using MeDaUmFilme.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeDaUmFilme
{
    public static class TwitterMvcExtensions
    {
        public static void AddTwitter(this IServiceCollection services, IConfigurationSection config)
        {
            services.AddScoped<Twitter.Twitter>();
            services.AddSingleton(new TwitterConfig(config.GetValue<string>("ConsumerKey"),
                config.GetValue<string>("ConsumerSecret"),
                config.GetValue<string>("AccessToken"),
                config.GetValue<string>("AccessTokenSecret")));
        }
    }
}