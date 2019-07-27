[![Build Status](https://dev.azure.com/excellaco/azure-stream-analysis-pipelines/_apis/build/status/excellalabs.azure-stream-analysis)](https://dev.azure.com/excellaco/azure-stream-analysis-pipelines/_build/latest?definitionId=2)

# Real Time Data Streaming of Raspberry PI and Twitter Data

Setting up near-real time data pipelines in Azure, with Azure Stream Analytics allows one to quickly scale and perform advanced analytics on moving data. The following repository demonstrates how we easily utilize the following Azure components to easily spin up a data pipeline to stream data from multiple sources using serverless functionality in Azure.

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

### Sentiment Analysis of a Twitter Feed
This repo demonstrates the ability to stream Twitter data based on keywords or specific users utilizing server-less functionality suchas Azure Logic Apps, EventHub and Stream Analytics. The egressed and analysed data can be vizualized in a Power BI dashboard. 

### Streaming of Raspberry PI Data
This repo demonstrates the ability to stream IoT sensor data from a Raspberry PI to the cloud, utilizing an EventHub, a Stream Analytics jon, an Azure Service Bus and and an Azure Logic App. The sensor data can be viewed in a Power BI dashboard, but there is also built in functionality to demonstrate how easy it is to set up your own burglar alarm. Any motion detected by the Raspberry PI's sensors will put a message in a service bus queue that will be picked up by an Azure Logic App which in turn will send a notification e-mail to a given e-mail address. The sample demonstrates the LAG functionality in particular, but also how to use reference data to enrich the stream. 

## Solution Architecture

### Sentiment Analysis of a Twitter Feed
![Solution Architecture](https://www.lucidchart.com/publicSegments/view/69aabaa9-865d-42e1-b175-96a9639cc2a9/image.png)

### Streaming of Raspberry PI Data
![Solution Architecture Streaming of Raspberry PI data](https://github.com/excellalabs/azure-stream-analysis/blob/master/Raspberry%20PI%20Stream.png)

## How to get started

### Sentiment Analysis of a Twitter Feed
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-and-run-semantic-analysis.md) to setup and run semantic analysis

### Streaming of Raspberry PI Data
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-and-run-raspberrypi-stream.md)
to setup and run Raspberry PI sensor streaming

## Contribute
See anything you want to improve? Do you want to build out the existing code base? Don't heistate to open a PR!
Please read our [contributing guidelines](https://github.com/excellalabs/azure-stream-analysis/blob/master/contributing.md)


