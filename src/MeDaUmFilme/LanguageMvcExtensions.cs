using MeDaUmFilme.Language;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeDaUmFilme
{
    public static class LanguageMvcExtensions
    {
        public static void AddLanguage(this IServiceCollection services, IConfigurationSection config)
        {
            services.AddSingleton<IAnalyzer>(new Analyzer(Guid.Parse(config.GetValue<string>("AppKey")), config.GetValue<string>("SubscriptionKey")));
        }
    }

    public static class MeDaUmFilmeExtensions
    {
        public static void AddMeDaUmFilme(this IServiceCollection services)
        {
            services.AddSingleton<IMeDaUmFilmeSearch>(new MeDaUmFilmeSearch());
        }
    }
}
