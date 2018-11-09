using AutoMoqCore;
using Excella.TwitterClient.Business.Twitter;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi.Models;
using Xunit;

namespace Excella.TwitterClient.Business.Tests
{
    public class TwitterStreamingClientTests
    {
        private static readonly Action<ITweet> tweetAction = (t) => { };

        public class Constructor
        {
            private static readonly AutoMoqer mocker = new AutoMoqer();
            private static readonly Mock<ILoggerFactory> mockLoggerFactory = mocker.GetMock<ILoggerFactory>();
            private static readonly Mock<ITwitterStreamingApiAdapter> mockTwitterStreamingApiAdapter = mocker.GetMock<ITwitterStreamingApiAdapter>();

            public static IEnumerable<object[]> Data_GivenAnyParameterIsNull =>
                new List<object[]>
                {
                    new object[] { null, mockTwitterStreamingApiAdapter.Object, "loggerFactory" },
                    new object[] { mockLoggerFactory.Object, null, "twitterStreamingApiAdapter" },
                    new object[] { null, null, "loggerFactory" },
                };

            [Theory]
            [MemberData(nameof(Data_GivenAnyParameterIsNull))]
            public void GivenAnyParameterIsNull_ShouldThrowArgumentNullException(ILoggerFactory loggerFactory,
                ITwitterStreamingApiAdapter twitterStreamingApiAdapter, string missingParam)
            {
                Action act = () => new TwitterStreamingClient(loggerFactory, twitterStreamingApiAdapter);

                act.Should().ThrowExactly<ArgumentNullException>().And.ParamName.Should().Be(missingParam);
            }
        }

        public class GetUserNames
        {
            private readonly TwitterStreamingClient unitUnderTest;
            private readonly AutoMoqer mocker = new AutoMoqer();
            public GetUserNames()
            {
                unitUnderTest = mocker.Create<TwitterStreamingClient>();
            }

            [Fact]
            public void GivenNull_ShouldReturnNull()
            {
                var actual = unitUnderTest.GetUserNames(null);

                actual.Should().BeNull();
            }

            [Theory]
            [InlineData()]
            [InlineData("user1", "user2")]
            public void GivenNotList_ShouldReturnListWithSameAmountOfUsers(params string[] userNames)
            {
                var actual = unitUnderTest.GetUserNames(userNames);

                actual.Should().HaveSameCount(userNames);
            }
        }

        public class StartStream
        {
            private readonly TwitterStreamingClient unitUnderTest;
            private readonly AutoMoqer mocker = new AutoMoqer();
            private readonly Mock<ITwitterStreamingApiAdapter> mockTwitterStreamingApiAdapter;

            public StartStream()
            {
                mockTwitterStreamingApiAdapter = mocker.GetMock<ITwitterStreamingApiAdapter>();
                unitUnderTest = mocker.Create<TwitterStreamingClient>();
            }
            public static IEnumerable<object[]> Data_GivenNullOrEmptyFilters =>
                new List<object[]>
                {
                    new object[] { null, null, null },
                    new object[] { new string[] { }, null, null },
                    new object[] { null, new string [] { }, null },
                    new object[] { new string[] { }, new string [] { }, null },
                    new object[] { null, null, tweetAction },
                    new object[] { new string[] { }, null, tweetAction },
                    new object[] { null, new string [] { }, tweetAction },
                    new object[] { new string[] { }, new string [] { }, tweetAction }
                };

            [Theory]
            [MemberData(nameof(Data_GivenNullOrEmptyFilters))]
            public void GivenNullOrEmptyFilters_ShouldThrowInvalidOperationException(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                Action act = () => unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                act.Should().ThrowExactly<InvalidOperationException>().And.Message.Should().Contain("filters");
            }

            public static IEnumerable<object[]> Data_GivenOneOrMoreFiltersAndNullAction =>
                new List<object[]>
                {
                    new object[] { new string[] { "user1" }, null, null },
                    new object[] { new string[] { "user1" }, new string[] { }, null },
                    new object[] { null, new string[] { "keyword1" }, null },
                    new object[] { new string[] { }, new string[] { "keyword1" }, null },
                    new object[] { new string[] { "user1" }, new string[] { "keyword1" }, null }
                };

            [Theory]
            [MemberData(nameof(Data_GivenOneOrMoreFiltersAndNullAction))]
            public void GivenOneOrMoreFiltersAndNullAction_ShouldThrowInvalidOperationException(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                Action act = () => unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                act.Should().ThrowExactly<InvalidOperationException>().And.Message.Should().Contain("action");
            }

            public static IEnumerable<object[]> Data_GivenOneOrMoreFiltersAndNotNullAction =>
                new List<object[]>
                {
                    new object[] { new string[] { "user1" }, null, tweetAction },
                    new object[] { new string[] { "user1" }, new string[] { }, tweetAction },
                    new object[] { null, new string[] { "keyword1" }, tweetAction },
                    new object[] { new string[] { }, new string[] { "keyword1" }, tweetAction },
                    new object[] { new string[] { "user1" }, new string[] { "keyword1" }, tweetAction }
                };

            [Theory]
            [MemberData(nameof(Data_GivenOneOrMoreFiltersAndNotNullAction))]
            public void GivenOneOrMoreFiltersAndNotNullAction_ShouldStartStream(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                mockTwitterStreamingApiAdapter.Verify(t => t.StartStreamMatchingAllConditionsAsync());
            }

            public static IEnumerable<object[]> Data_GivenOneUserAndNotNullAction =>
                new List<object[]>
                {
                    new object[] { new string[] { "user1" }, null, tweetAction },
                    new object[] { new string[] { "user1" }, new string[] { }, tweetAction },
                    new object[] { new string[] { "user1" }, new string[] { "keyword1" }, tweetAction }
                };

            [Theory]
            [MemberData(nameof(Data_GivenOneUserAndNotNullAction))]
            public void GivenOneUserAndNotNullAction_ShouldCallAddFollow(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                mockTwitterStreamingApiAdapter.Verify(t => t.AddFollow(It.IsAny<IUser>(), tweetAction), Times.Exactly(users.Count()));
            }

            [Theory]
            [MemberData(nameof(Data_GivenOneUserAndNotNullAction))]
            public void GivenOneUserAndNotNullAction_ShouldNotCallAddTrack(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                mockTwitterStreamingApiAdapter.Verify(t => t.AddTrack(It.IsAny<string>(), It.IsAny<Action<ITweet>>()), Times.Never);
            }

            public static IEnumerable<object[]> Data_GivenOneKeywordAndNullOrEmptyUsersAndNotNullAction =>
                new List<object[]>
                {
                    new object[] { null, new string[] { "keyword1" }, tweetAction },
                    new object[] { new string[] {  }, new string[] { "keyword1" }, tweetAction },
                };

            [Theory]
            [MemberData(nameof(Data_GivenOneKeywordAndNullOrEmptyUsersAndNotNullAction))]
            public void GivenOneKeywordAndNullOrEmptyUsersAndNotNullAction_ShouldCallAddTrack(IEnumerable<string> users, IEnumerable<string> keywords, Action<ITweet> tweetAction)
            {
                unitUnderTest.WithFilters(keywords).WithUsersToFollow(users).WithActionOnReceived(tweetAction).StartStream();

                mockTwitterStreamingApiAdapter.Verify(t => t.AddTrack(keywords.First(), tweetAction), Times.Exactly(keywords.Count()));
            }

        }
    }
}
