## How to get started
To get started streaming data from a Twitter feed, please follow to steps below.

### Twitter 
To connect to Twitter:
1. Create a Twitter developer account https://developer.twitter.com/
2. Create a Twitter [application](https://developer.twitter.com/en/docs/basics/developer-portal/guides/apps.html)
3. For your application, locate the Account Secret and Account Token to be used for connecting to the API

### Azure
Please refer to the [following guide](https://github.com/excellaco/azure-sentiment-analysis/blob/master/deploy-azure-infrastructure.md) to deploy the Azure Infrastructure

#### Azure Functions
Please refer to the [following guide](https://github.com/excellaco/azure-sentiment-analysis/blob/master/deploy-azure-functions.md) to deploy the required function apps used in this sample.

#### Azure WebJob
The Twitter feeds are consumed by an Azure WebJob that retains the connection to the Twitter API. To deploy the WebJob, please:
1. Open the WebJob [solution](https://github.com/excellaco/azure-sentiment-analysis/tree/master/src/webjob) in VS Code or Visual Studio.
2. Right click on the WebJob project and select to deploy as a WebJob
3. Select to publish to exiting
4. Log in with your Azure credentials
5. Select the existing WebJob to publish to
6. Select Publish

Once deployed, navigate to the WebJob in the Azure portal and make sure to update the application configuration values accordingly for the EventHub, Cognitive Services and Twitter secret parameters.


| Key                        | Value                                                      |
| ---------------------------| ---------------------------------------------------------- |
| Tweet_Group_Key | Used as a grouping for Tweets (e.g. if you stream different keywords for the same project |
| Twitter_Access_Token | Twitter Access Token, locted in the Twitter Developer console |
| Twitter_Access_Token_Secret | Twitter Access Token Secret, located in the Twitter Developer console |
| Twitter_Consumer_Key | Twitter Consumer Key, located in the Twitter Developer console |
| Twitter_Consumer_Secret | Twitter Consumer Secret, located in the Twitter Developer console |
| Twitter_Keywords          | Comma-separated list of keywords to stream data for |
| Twitter_UserToFollow | Comma-separated list of Twitter usernames to follow |
| Cognitive_Service_Uri      | Cognitive service URI, located in the Cognitive service blade in Azure |
| Ocp_Apmin_Subscription_Key | Cognitive service subscription ID, located in the Cognitive service blade in Azure |
| EventHub_ConnectionString        | EventHub connection string, located in the Event Hub blade in Azure |
| EventHub_Path | Event Hub namespace name |
| Storage_Account_ConnectionString | Connection string to your Azure Storage Account, located in the Storage Account blade in Azure |
| ApplicationInsight_InstrumentKey | Application Insight instrumentation key, located in the Application Insight blade in Azure |

### Azure Stream Analytics SQL
Once the Azure Infrastructure has been deployed, open the Twitter Stream Analytics Job and paste in the Azure Stream Analytics query from [here](https://github.com/excellaco/azure-sentiment-analysis/blob/master/src/azure-stream-analytics/twitter-streaming-job)

### Power BI
Please refer to the [following guide](https://github.com/excellaco/azure-sentiment-analysis/blob/master/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset

### Run
To start streaming Twitter feeds, please:
1. Navigate to the WebJob and specify the keyword(s) to stream feeds for in the application settings
2. Start the Azure WebJob
3. Start the Function App if it is not already running
4. Start Twitter Azure Stream Analytics Job (takes a couple of minutes to start running)
