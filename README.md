[![Build Status](https://dev.azure.com/excellaco/azure-stream-analysis-pipelines/_apis/build/status/excellalabs.azure-stream-analysis)](https://dev.azure.com/excellaco/azure-stream-analysis-pipelines/_build/latest?definitionId=2)

# Real Time Data Streaming of Raspberry PI and Twitter Data

Setting up real time data pipelines in Azure, with Azure Stream Analytics allows one to quickly scale and perform advanced analytics on a moving data stream. The following repository demonstrates how we easily can utilize various Azure services to easily spin up a data pipeline to stream data from multiple sources using serverless functionality in Azure.

### Technologies utilized in this repo
* Azure Logic Apps
* Azure Service Bus
* Azure EventHub
* Azre Stream Analytics
* Azure DevOps
* Power BI
* Raspberry PI
* GrovePi

## Features

### Streaming a Twitter Feed and performing Sentiment Analysis
This repo demonstrates the ability to stream Twitter data based on keywords or specific users utilizing server-less functionality suchas Azure Logic Apps, EventHub and Stream Analytics. The egressed and analysed data can be vizualized in a Power BI dashboard. 

![Solution Architecture](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/img/Solution%20Architecture%20-%20Twitter.png)

### Streaming of Raspberry PI IoT Data
This repo demonstrates the ability to stream IoT sensor data from a Raspberry PI to the cloud, utilizing an EventHub, a Stream Analytics jon, an Azure Service Bus and and an Azure Logic App. The sensor data can be viewed in a Power BI dashboard, but there is also built in functionality to demonstrate how easy it is to set up your own burglar alarm. Any motion detected by the Raspberry PI's sensors will put a message in a service bus queue that will be picked up by an Azure Logic App which in turn will send a notification e-mail to a given e-mail address. The sample demonstrates the LAG functionality in particular, but also how to use reference data to enrich the stream. 

![Solution Architecture Streaming of Raspberry PI data](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/img/Solution%20Architecture%20-%20Raspberry%20PI.png)

## How to get started

### Sentiment Analysis of a Twitter Feed
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-and-run-semantic-analysis.md) to setup and run semantic analysis

### Streaming of Raspberry PI Data
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-and-run-raspberrypi-stream.md)
to setup and run Raspberry PI sensor streaming

## Contribute
See anything you want to improve? Do you want to build out the existing code base? Don't heistate to open a PR!
Please read our [contributing guidelines](https://github.com/excellalabs/azure-stream-analysis/blob/master/contributing.md)


