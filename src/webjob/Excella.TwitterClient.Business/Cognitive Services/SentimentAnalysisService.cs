using Excella.TwitterClient.Core.Configuration;
using Excella.TwitterClient.Core.SentimentAnalysis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Excella.TwitterClient.Business.Cognitive_Services
{
    public interface ISentimentAnalysisService
    {
        Task<decimal> GetSentimentScore(string text);
    }

    public sealed class SentimentAnalysisService : ISentimentAnalysisService
    {
        private readonly ILogger logger;
        private readonly SentimentAnalysisSettings sentimentAnalysisSettings;

        public SentimentAnalysisService(ILoggerFactory loggerFactory, SentimentAnalysisSettings sentimentAnalysisSettings)
        {
            this.logger = loggerFactory.CreateLogger(nameof(SentimentAnalysisService));
            this.sentimentAnalysisSettings = sentimentAnalysisSettings;
        }

        public async Task<decimal> GetSentimentScore(string text)
        {
            var client = CreateHttpClient();
            var byteData = CreateRequest(text);
            var uri = sentimentAnalysisSettings.CognitiveServiceUri;

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                logger.LogInformation("Requesting a sentiment score");

                var response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogError($"Call to cognitive service failed: {await response.Content.ReadAsStringAsync()}");
                    return 0;
                }

                var sentimentResponse = JsonConvert.DeserializeObject<SentimentAnalysis<Response>>(await response.Content.ReadAsStringAsync());
                var sentimentScore = sentimentResponse.Documents.FirstOrDefault().Score;

                logger.LogInformation($"Sentiment score received with value {sentimentScore}");
                return sentimentScore;
            }
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            // Request headers
            var subscriptionKey = sentimentAnalysisSettings.OcpApminSubscriptionKey;
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            return client;
        }

        private byte[] CreateRequest(string text)
        {
            logger.LogInformation("Creating sentiment score request");

            var sentimentRequest = new SentimentAnalysis<Request> { Documents = new List<Request> { new Request { Text = text } } };
            var payload = JsonConvert.SerializeObject(sentimentRequest);

            return Encoding.UTF8.GetBytes(payload);
        }
    }
}
