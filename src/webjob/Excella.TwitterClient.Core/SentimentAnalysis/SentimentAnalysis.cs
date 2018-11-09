using System.Collections.Generic;

namespace Excella.TwitterClient.Core.SentimentAnalysis
{
    public class SentimentAnalysis<T>
    {
        public List<T> Documents { get; set; } = new List<T>();
    }

    public class Request
    {
        public int Id => 1;
        public string Text { get; set; }
        public string Language => "en";
    }

    public class Response
    {
        public int Id { get; set; }
        public decimal Score { get; set; }
    }
}
