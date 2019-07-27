## How to get started
To get started streaming data from a Twitter feed, please follow to steps below.

### Prerequisites
Please ensure you have the following
- [Twitter account](www.twitter.com)
- [Azure subscription](https://azure.microsoft.com/en-us/free/)
- [PowerBI](https://powerbi.microsoft.com/en-us/)

###  Deploy Azure Infrastructure

#### 1. Deploy Azure Resource Management Template
- Navigate to [deploy an ARM template](https://portal.azure.com/#create/Microsoft.Template)
- Click on "Build your own Template in the Editor"
- Copy and paste the [Raspberry PI ARM Template](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy/azure-stream-analytics--twitter-template.json)
- Click "Save"

#### 2. Enter valid parameter values
- Select to create a new resource group, or utilize an existing.
- Enter the required template parameters such as Twitter handle and Power BI account

#### 3. Deploy
Select to agree with terms and conditions and click "Purchase" to trigger the deployment.

#### Authenticate accounts
The ARM template will succesfully set up the required infrastructure but will require you to authenticate you Twitter and PowerBI accounts in to fully function.

1. Authenticate Power BI output
- Navigate to your 
2. Authenticate Twitter listener

### Power BI
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset

### Run
- Navigate to your twitter-listener Logic App
- Open the Twitter connection step
- Enter the keywords to subscribe to
- Enable your twitter-listener Logic App
- Navigate to your Azure Stream Analytics job
- Start your Stream Analytics job
