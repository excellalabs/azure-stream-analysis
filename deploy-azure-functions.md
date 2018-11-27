 
## How to get started
Please refer to the following guide to publish the function apps part of this repo.

### Publish Azure Functions
To publish the required function apps in this repo, please follow the following steps:

1. Open the [function app solution](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/azure-functions/Excella.Twitter.AzureFunction.sln) in Visual Studio
2. Right-click on the solution in the solution explorer 
3. Select Publish
4. Select Publish to existing
5. Select Publish
6. Log in with you Azure credentials
7. Select the Function App previously created when deploying the Azure Infrastructure
8. Click Publish

#### Configure Application Settings
The following configuration values may need to be configured for the Azur Functions to work correctly

| Key                        | Value                                                      | Required for |
| ---------------------------| ---------------------------------------------------------- | ------------ |
| Send_From                  | E-mail that Azure Functions will use as sending e-mail     | AnomalyDetectionFunction, ThresHoldFunction |
| Send_To                    | E-mail that the Azure Functions will use to send notifications to | AnomalyDetectionFunction, ThresHoldFunction |
| SendGrid_API_Key           | SendGrid API key to be used to send e-mails |  AnomalyDetectionFunction, ThresHoldFunction |
| Twilio_Account_Id          | Twilio Account Id | MotionDetectionFunction |
| Twilio_Auth_Token          | Twilio Auth Token | MotionDetectionFunction | 
| Twilio_Caller_PhoneNumber  | Phone number that Twilio will call from | MotionDetectionFunction |
| Twilio_To_PhoneNumber      | Phone number that Twilio will call | MotionDetectionFunction |
| Twilio_Resource_Url        | Twilio resource URI for TwiML resources (e.g. configure text-to-speech) | MotionDetectionFunction |
  
#### Azure Stream Analytics
The Function Appare not part of the ARM template, as they require the Azure Functions to be published first. After the ARM template has been deployed, and the Azure Functions have been published, please add Azure Function output for the following outputs:

Twitter Stream Analytics Job
- AnomalyDetectionFunction
- ThresHoldFunction

Raspberry PI Analytics Job
- MotionDetectionFunction

### Twilio
Twilo is used to make a phone call as part of the MotionDetectionFunction.
To create a Twilio account to get access to the necessary configuration parameters, please visit https://www.twilio.com/

You'll need to create a [TwiML Bin](https://www.twilio.com/blog/2017/11/twiml-bins-a-serverless-and-codeless-way-to-try-twilio.html) in order to use text-to-speech. Once created, please copy-and-paste the following resource

```
<?xml version="1.0" encoding="UTF-8"?>
<Response>
  <Say voice="man">Someone has entered your home, please return immediately!</Say>
  <Play>https://demo.twilio.com/docs/classic.mp3</Play>
</Response>
```

### Sendgrid
SendGrid is used to send e-mail notifications by the ThresholdFunction and the AnomalyDetectionFunction.
If you do not already have a SendGrid account in Azure, please do the following:

1. In Azure, select to create a new resource
2. Search for SendGrid
3. Select SendGrid Email Delivery
4. Click Create
5. Enter a name and a password and other required parameters
6. Click create
