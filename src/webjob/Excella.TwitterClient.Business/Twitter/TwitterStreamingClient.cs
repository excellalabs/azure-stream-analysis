using Excella.TwitterClient.Core.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;

namespace Excella.TwitterClient.Business.Twitter
{
    public interface ITwitterStreamingClient
    {
        ITwitterStreamingClient CreateStream();

        ITwitterStreamingClient WithFilters(IEnumerable<string> keywords);

        ITwitterStreamingClient WithUsersToFollow(IEnumerable<string> userNames);

        ITwitterStreamingClient WithActionOnReceived(Action<ITweet> receivedAction);

        void StartStream();

        void StopStream();
    }

    public sealed class TwitterStreamingClient : ITwitterStreamingClient
    {
        private IEnumerable<string> keywords;
        private Action<ITweet> actionOnReceived;
        private IEnumerable<IUser> users;
        private readonly ILogger logger;
        private readonly ITwitterStreamingApiAdapter twitterStreamingApiAdapter;

        public TwitterStreamingClient(ILoggerFactory loggerFactory, ITwitterStreamingApiAdapter twitterStreamingApiAdapter)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException("loggerFactory");
            }
            this.logger = loggerFactory.CreateLogger(nameof(TwitterStreamingClient));
            this.twitterStreamingApiAdapter = twitterStreamingApiAdapter ?? throw new ArgumentNullException("twitterStreamingApiAdapter");
        }

        public ITwitterStreamingClient CreateStream()
        {
            twitterStreamingApiAdapter.CreateStream();
            return this;
        }

        public ITwitterStreamingClient WithFilters(IEnumerable<string> filters)
        {
            this.keywords = filters;
            return this;
        }

        public ITwitterStreamingClient WithUsersToFollow(IEnumerable<string> userNames)
        {
            this.users = GetUserNames(userNames);
            return this;
        }

        public IEnumerable<IUser> GetUserNames(IEnumerable<string> userNames)
        {
            return userNames?.Select(x => twitterStreamingApiAdapter.GetUserFromScreenName(x));
        }

        public ITwitterStreamingClient WithActionOnReceived(Action<ITweet> actionOnReceived)
        {
            this.actionOnReceived = actionOnReceived;
            return this;
        }

        public void StartStream()
        {
            if (NoFiltersDefined()) throw new InvalidOperationException("No filters have been defined");

            if (this.actionOnReceived == null) throw new InvalidOperationException("No action on received tweet has been defined");

            if (this.users != null && this.users.Any())
            {
                this.users.ToList().ForEach(userToFollow => twitterStreamingApiAdapter.AddFollow(userToFollow, this.actionOnReceived));
            }
            else
            {
                this.keywords.ToList().ForEach(keyword => twitterStreamingApiAdapter.AddTrack(keyword, this.actionOnReceived));
            }

            twitterStreamingApiAdapter.StartStreamMatchingAllConditionsAsync();
        }

        private bool NoFiltersDefined()
        {
            return (this.keywords == null || !this.keywords.Any()) && (this.users == null || !this.users.Any());
        }

        public void StopStream()
        {
            twitterStreamingApiAdapter.StopStream();
        }
    }
}
