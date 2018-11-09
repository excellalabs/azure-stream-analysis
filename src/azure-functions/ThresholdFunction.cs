using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Collections.Generic;
using System;

namespace Excella.Twitter.AzureFunction
{
    public static class ThresholdFunction
    {
        [FunctionName("ThresholdFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.Info(requestBody);

            var body = data?[0]["tweettext"];
            var sentimentScore = data?[0]["sentimentscore"];

            log.Info($"Tweet: {body}");
            log.Info($"SentimentScore: {sentimentScore}");

            log.Info("Creating e-mail to send");

            var apiKey = GetEnvironmentVariable("SendGrid_API_Key");
            var sendTo = GetEnvironmentVariable("Send_To");
            var sendFrom = GetEnvironmentVariable("Send_From");

            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(sendFrom, "Excella Sentiment Analysis"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(sendTo),
            };

            msg.AddTos(recipients);

            msg.SetSubject("Warning - Sentiment Analysis");

            msg.AddContent(MimeType.Text, $"A negative tweet was just recieved with the sentiment score of {sentimentScore}. The tweet contained the following text: {body}");
            log.Info("Sending e-mail");
            var response = client.SendEmailAsync(msg);

            return new OkObjectResult("Notification sent");
        }

        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
