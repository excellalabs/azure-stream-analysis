using System;
using System.Collections.Generic;
using System.Text;

namespace Excella.TwitterClient.Core
{
    public sealed class Tweet
    {
        public long Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string TweetText { get; set; }
        public string Keyword { get; set; }
        public string TwitterHandle { get; set; }
        public decimal SentimentScore { get; set; }
        public int RetweetCount { get; set; }
        public int FavoriteCount { get; set; }
        public List<string> HashTags { get; set; }
        public string Language { get; set; }
        public string CountryCode { get; set; }
        public string TweetGroupKey { get; set; }
    }
}
