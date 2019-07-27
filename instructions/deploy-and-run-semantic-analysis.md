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
- Enter the required template parameters:
    - Twitter handle
    - Power BI user name
    - Power BI display name

#### 3. Deploy
Select to agree with terms and conditions and click "Purchase" to trigger the deployment.

#### Authenticate accounts
The ARM template will succesfully set up the required infrastructure but will require you to authenticate you Twitter and PowerBI accounts in to fully function.

1. Authenticate Power BI output
- Navigate to your Azure Stream Analytics Job
- Click on the powerbi output
- Click the blue button "Renew Authorization"
- Log-in using your Power BI user account
- Click "Save"
- Navigate back to the Stream Analytics overview page
- Click "Start" to start your streaming analyticsc job

2. Authenticate Twitter listener
- Navigate to your Azure Logic App (twitter-listener)
- Click on "Edit"
- Click on the Twitter connection step (first step)
- Click on the invalid connection symbol
- Log-in using your Twitter account
- Navigate back to the Logic App overview page
- Click "Enable" to enable your trigger

**Note that you are able to change which keyword to subscribe to in the first twitter connection step**

### Power BI
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset

