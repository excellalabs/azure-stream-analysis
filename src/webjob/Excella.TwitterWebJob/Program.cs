using Excella.TwitterClient.Business;
using Excella.TwitterClient.Business.Cognitive_Services;
using Excella.TwitterClient.Business.EventHub;
using Excella.TwitterClient.Business.Twitter;
using Excella.TwitterWebJob.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

namespace Excella.TwitterWebJob
{
    class Program
    {
        private static IStreamManager streamManager;

        static void Main()
        {
            var appSettings = GetAppSettings();
            var jobHostConfig = new JobHostConfiguration();

            if (jobHostConfig.IsDevelopment)
            {
                jobHostConfig.UseDevelopmentSettings();
            }

            using (var loggerFactory = new LoggerFactory())
            {
                SetUpConfig(jobHostConfig, loggerFactory, appSettings.JobHostConfigurationSettings);

                StartTwitterStream(loggerFactory, appSettings);

                var host = new JobHost(jobHostConfig);
                // The following code ensures that the WebJob will be running continuously
                host.RunAndBlock();
            }
        }

        public static void StartTwitterStream(ILoggerFactory loggerFactory, AppSettings appSettings)
        {
            streamManager = new StreamManager(
                new TwitterStreamingClient(loggerFactory, new TwitterStreamingApiAdapter(appSettings.TwitterSettings)),
                new SentimentAnalysisService(loggerFactory, appSettings.SentimentAnalysisSettings),
                new EventHubService(loggerFactory, appSettings.EventHubSettings),
                loggerFactory, appSettings.TwitterSettings);

            streamManager.StartTwitterStream();
        }

        private static void SetUpConfig(JobHostConfiguration config, LoggerFactory loggerFactory,
            JobHostConfigurationSettings jobHostConfigurationSettings)
        {
            config.DashboardConnectionString = ""; //Disable
            config.StorageConnectionString = jobHostConfigurationSettings.StorageAccountConnectionString;

            config.LoggerFactory = loggerFactory.AddConsole()
                .AddApplicationInsights(jobHostConfigurationSettings.ApplicationInsightInstrumentKey, null);

            config.Tracing.ConsoleLevel = TraceLevel.Off;
        }

        private static AppSettings GetAppSettings()
        {
            var appConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var appSettings = appConfig.Get<AppSettings>();
            return appSettings;
        }

    }
}
