using System;

namespace Elite3E.Infrastructure.Configuration
{
    using System.IO;
    using Elite3E.Infrastructure.Models;
    using Microsoft.Extensions.Configuration;

    public class ApplicationConfigurationBuilder
    {
        private static AppSettings _instance;
        public static AppSettings Instance => _instance ?? Create();

        private static AppSettings Create()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrEmpty(environment))
                environment = "local";

            var contentRoot = Directory.GetCurrentDirectory();

            var appsettingsFilename = "appsettings";
            var jsonExtension = ".json";

            // Below ensures it merges the original appsettings.json with any environment appsettings.
            var config = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile($"{appsettingsFilename}{jsonExtension}")
                .AddJsonFile($"{appsettingsFilename}.{environment}{jsonExtension}", true)
                .AddEnvironmentVariables()
                .Build();

            return _instance = config.GetSection(appsettingsFilename).Get<AppSettings>();
        }
    }
}

