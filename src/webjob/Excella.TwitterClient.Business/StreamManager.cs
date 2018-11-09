using Excella.TwitterClient.Business.Cognitive_Services;
using Excella.TwitterClient.Business.EventHub;
using Excella.TwitterClient.Business.Twitter;
using Excella.TwitterClient.Core.Configuration;
using Microsoft.Extensions.Logging;
using Tweetinvi.Models;

namespace Excella.TwitterClient.Business
{
    public interface IStreamManager
    {
        void StartTwitterStream();
    }

    public sealed class StreamManager : IStreamManager
    {
        private readonly ITwitterStreamingClient twitterStreamingClient;
        private readonly ISentimentAnalysisService sentimentAnalysisService;
        private readonly IEventHubService eventHubService;
        private readonly ILogger logger;
        private readonly TwitterSettings twitterSettings;

        public StreamManager(
            ITwitterStreamingClient twitterStreamingClient,
            ISentimentAnalysisService sentimentAnalysisService,
            IEventHubService eventHubService,
            ILoggerFactory loggerFactory,
            TwitterSettings twitterSettings)
        {
            this.twitterStreamingClient = twitterStreamingClient;
            this.sentimentAnalysisService = sentimentAnalysisService;
            this.eventHubService = eventHubService;
            this.twitterSettings = twitterSettings;

            this.logger = loggerFactory.CreateLogger(nameof(StreamManager));
        }

        public void StartTwitterStream()
        {
            this.twitterStreamingClient
                .CreateStream()
                .WithFilters(twitterSettings.Keywords)
                .WithUsersToFollow(twitterSettings.UsersToFollow)
                .WithActionOnReceived(OnTweetReceived)
                .StartStream();
        }

        private async void OnTweetReceived(ITweet tweet)
        {
            logger.LogInformation($"Tweet received with message {tweet.Text}");

            var sentimentScore = await this.sentimentAnalysisService.GetSentimentScore(tweet.Text);

            await this.eventHubService.SendMessagesToEventHub(tweet, sentimentScore);
        }

    }
}
