## How to get started
To get started streaming data from a Twitter feed, please follow to steps below.

### Prerequisites
- Twitter account
- Power BI acccount
- [Azure subscription](https://azure.microsoft.com/en-us/free/)

###  Deploy Azure Infrastructure

#### 1. Deploy Azure Resource Management Template

- Navigate to deploy an ARM template [here](https://portal.azure.com/#create/Microsoft.Template)
- Click on "Build your own Template in the Editor"
- Copy and paste the [Raspberry PI ARM Template](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy/azure-stream-analytics--twitter-template.json)
- Click "Save"

#### 2. Enter valid parameter values
- Select to create a new resource group, or utilize an existing.
- Enter the required template parameters such as Twitter handle and Power BI account

#### 3. Deploy
Select to agree with terms and conditions and click "Purchase" to trigger the deployment.

#### Authenticate accounts
The ARM template will succesfully set up the required infrastructure but will require you to authenticate you Twitter and PowerBI account to fully function. 

### Power BI
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset

### Run
- Select the keywords to listen for in your twitter-listener Logic App
- Enable your twitter-listener Logic App
- Start your Stream Analytics job
