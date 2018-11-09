using Excella.TwitterClient.Core;
using Excella.TwitterClient.Core.Configuration;
using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Models;

namespace Excella.TwitterClient.Business.EventHub
{
    public interface IEventHubService
    {
        Task SendMessagesToEventHub(ITweet tweet, decimal sentimentScore);
    }

    public sealed class EventHubService : IEventHubService
    {
        private readonly EventHubClient eventHubClient;
        private readonly ILogger logger;
        private readonly EventHubSettings eventHubSettings;

        public EventHubService(ILoggerFactory loggerFactory, EventHubSettings eventHubSettings)
        {
            this.logger = loggerFactory.CreateLogger(nameof(EventHubService));
            this.eventHubSettings = eventHubSettings;
            this.eventHubClient = InitializeEventHub();
        }

        public async Task SendMessagesToEventHub(ITweet tweet, decimal sentimentScore)
        {
            var groupKey = eventHubSettings.TweetGroupKey;

            var payLoad = new Tweet
            {
                Id = tweet.Id,
                CreatedTime = tweet.CreatedAt,
                TweetText = tweet.Text,
                SentimentScore = sentimentScore,
                FavoriteCount = tweet.FavoriteCount,
                RetweetCount = tweet.RetweetCount,
                Language = tweet.Language.ToString(),
                CountryCode = tweet.Place != null ? tweet.Place.CountryCode : "Undefined",
                HashTags = tweet.Hashtags.ToList().Select(x => x.Text).ToList(),
                TweetGroupKey = groupKey
            };

            var message = JsonConvert.SerializeObject(payLoad);
            logger.LogInformation("Calling EventHub");

            await this.eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));

            logger.LogInformation("EventHub called succesfully");
        }

        private EventHubClient InitializeEventHub()
        {
            logger.LogInformation("Creating EventHub");

            var ehConnectionString = eventHubSettings.EventHubConnectionString;
            var entityPath = eventHubSettings.EventHubPath;

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(ehConnectionString)
            {
                EntityPath = entityPath
            };
            return EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
    }
}
