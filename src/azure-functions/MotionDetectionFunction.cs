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
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace Excella.Twitter.AzureFunction
{
    public static class MotionDetectionFunction
    {
        [FunctionName("MotionDetectionFunction")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("Placing call");

            var accountId = GetEnvironmentVariable("Twilio_Account_Id");
            var authToken = GetEnvironmentVariable("Twilio_Auth_Token");
            var fromNumber = GetEnvironmentVariable("Twilio_Caller_PhoneNumber");
            var toNumber = GetEnvironmentVariable("Twilio_Caller_PhoneNumber");
            var resourceUrl = GetEnvironmentVariable("Twilio_Resource_Url");
            
            TwilioClient.Init(accountId, authToken);

            var to = new PhoneNumber(toNumber);
            var from = new PhoneNumber(fromNumber);
            var call = CallResource.Create(to, from,
                url: new Uri(resourceUrl));

            log.Info("Call made");

            return new OkObjectResult("Call made");
        }

        public static string GetEnvironmentVariable(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }
}
