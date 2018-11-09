using Excella.TwitterClient.Core.Configuration;

namespace Excella.TwitterWebJob.Configuration
{
    public class AppSettings
    {
        public TwitterSettings TwitterSettings { get; set; }
        public SentimentAnalysisSettings SentimentAnalysisSettings { get; set; }
        public EventHubSettings EventHubSettings { get; set; }
        public JobHostConfigurationSettings JobHostConfigurationSettings { get; set; }
    }
}
