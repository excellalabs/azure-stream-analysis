using Excella.TwitterClient.Core.Configuration;
using System;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Streaming;

namespace Excella.TwitterClient.Business.Twitter
{
    public interface ITwitterStreamingApiAdapter
    {
        void CreateStream();
        void StopStream();
        IUser GetUserFromScreenName(string userName);
        void StartStreamMatchingAllConditionsAsync();
        void AddTrack(string keyword, Action<ITweet> actionOnReceived);
        void AddFollow(IUser userToFollow, Action<ITweet> actionOnReceived);
    }

    public class TwitterStreamingApiAdapter : ITwitterStreamingApiAdapter
    {
        private static IFilteredStream stream;
        private readonly TwitterSettings twitterSettings;

        public TwitterStreamingApiAdapter(TwitterSettings twitterSettings)
        {
            this.twitterSettings = twitterSettings ?? throw new ArgumentNullException("twitterSettings");
        }

        public void AddFollow(IUser userToFollow, Action<ITweet> actionOnReceived)
        {
            stream.AddFollow(userToFollow, actionOnReceived);
        }

        public void AddTrack(string keyword, Action<ITweet> actionOnReceived)
        {
            stream.AddTrack(keyword, actionOnReceived);
        }

        public void CreateStream()
        {
            var credentials = new TwitterCredentials(twitterSettings.ConsumerKey, twitterSettings.ConsumerSecret,
                twitterSettings.AccessToken, twitterSettings.AccessTokenSecret);

            Auth.SetCredentials(credentials);

            if (stream != null)
            {
                this.StopStream();
            }

            stream = Stream.CreateFilteredStream(credentials);
        }

        public IUser GetUserFromScreenName(string userName)
        {
            return User.GetUserFromScreenName(userName);
        }

        public void StartStreamMatchingAllConditionsAsync()
        {
            stream.StartStreamMatchingAllConditionsAsync();
        }

        public void StopStream()
        {
            stream.StopStream();
            stream = null;
        }
    }
}
