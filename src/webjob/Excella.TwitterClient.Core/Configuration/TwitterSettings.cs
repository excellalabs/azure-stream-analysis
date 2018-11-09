using System.Collections.Generic;

namespace Excella.TwitterClient.Core.Configuration
{
    public class TwitterSettings
    {
        public List<string> UsersToFollow { get; set; }
        public List<string> Keywords { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }
    }
}
