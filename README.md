[![Build Status](https://dev.azure.com/excellaco/azure-sentiment-analysis-pipelines/_apis/build/status/excellaco.azure-sentiment-analysis)](https://dev.azure.com/excellaco/azure-sentiment-analysis-pipelines/_build/latest?definitionId=1)

# Real Time Data Streaming of Raspberry PI and Twitter Data

Setting up near-real time data pipelines in Azure, with Azure Stream Analytics allows one to quickly scale and perform advanced analytics on moving data. The following repository demonstrates how we easily utilize the following Azure components to easily spin up a data pipeline to stream data from multiple sources

### Technologies utilized in this repo
* Azure WebJob
* Azure EventHub
* Azre Stream Analytics
* Azure Functions
* Azure DevOps
* Power BI
* Raspberry PI
* Twitter Streaming API
* GrovePi
* Twilio

## Features

### Sentiment Analysis of a Twitter Feed
This repo demonstrates the ability to stream Twitter data based on keywords or specific users utilizing Azure WebJobs, EventHub and Stream Analytics. The egressed and analysed data can be vizualized in a Power BI dashboard, or acted upon based on triggered Azure Functions that will send e-mail notifications through a Sendgrid API. The data is furthermore stored in a CosmosDB for later use.

### Streaming of Raspberry PI Data
This repo demonstrates the ability to stream sensor data from a Raspberry PI utilizing Azure EventHub and Stream Analytics. The sensor data can be viewed in a Power BI dashboard, but there is also built in functionality to demonstrate how easy it is to set up your on burglar alarm. Any motion detected by the Raspberry PI's sensors will trigger an Azure Function which will call the user using Twilio. The sample demonstrates the LAG functionality in particular, but also how to use reference data to enrich a stream. 

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


