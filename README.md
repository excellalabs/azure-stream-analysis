# Real-Time Data Streaming of Raspberry Pi and Twitter Data

Setting up real time data pipelines in Azure, with Azure Stream Analytics allows one to quickly scale and perform advanced analytics on a moving data stream. The following repository demonstrates how we easily can utilize various Azure services to easily spin up a data pipeline to stream data from multiple sources using serverless functionality in Azure.

### Technologies demonstrated
* Azure Logic Apps
* Azure Service Bus
* Azure EventHub
* Azure Stream Analytics
* Azure DevOps
* Power BI
* Raspberry Pi
* GrovePi

### Potential use-cases
There are endless potential use-cases in which this tech-stack can be proven useful. To mention a few, we could potentially stream:
- Social media and RSS feeds to determine sentiment and overall success of a newly released feature or product
- Data from live traffic cameras to determine current conditions and measure improvements after roadwork
- Data from live traffic cameras to determine potential benefits/losses of introducing or removing toll roads
- Financial transactions to determine fraudulent activities
- IoT sensors of various kinds in the field to determine and predict air quality in different cities across the world

## Resources
Please refer to the following blog posts, presentations and recorded talks for further information:
- [Getting started with Azure Stream Analytics](https://www.excella.com/insights/getting-started-with-azure-stream-analytics)
- [Near Real-Time Data Streaming with Azure Stream Analytics](https://www.excella.com/insights/near-real-time-data-streaming-with-azure-stream-analytics)
- [Real-Time Data Streaming with Azure Stream Analytics](https://excellalabs.com/talks/real-time-data-streaming-azure-stream-analytics/)
- [PowerPoint presentation](https://github.com/excellalabs/azure-stream-analysis/blob/master/presentation/Real-Time%20data%20Streaming%20with%20Azure%20Stream%20Analytics.pptx)

## Features

### Streaming a Twitter Feed and performing Sentiment Analysis
This repo demonstrates the ability to stream Twitter data based on keywords or specific users utilizing server-less functionality such as Azure Logic Apps, EventHub and Stream Analytics. The egressed and analyzed data can be visualized in a Power BI dashboard. 

![Solution Architecture](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/img/Solution%20Architecture%20-%20Twitter.png)

#### How to get started
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/deploy-and-run-semantic-analysis.md) to set up the infrastructure with Azure Resource Management Templates and run semantic analysis on Twitter Feeds

### Streaming of Raspberry Pi IoT Data
This repo demonstrates the ability to stream IoT sensor data from a Raspberry PI to the cloud, utilizing an EventHub, a Stream Analytics jon, an Azure Service Bus and an Azure Logic App. The sensor data can be viewed in a Power BI dashboard, but there is also built in functionality to demonstrate how easy it is to set up your own burglar alarm. Any motion detected by the Raspberry PI's sensors will put a message in a service bus queue that will be picked up by an Azure Logic App which in turn will send a notification e-mail to a given e-mail address. The sample demonstrates the LAG functionality in particular, but also how to use reference data to enrich the stream. 

![Solution Architecture Streaming of Raspberry PI data](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/img/Solution%20Architecture%20-%20Raspberry%20PI.png)

#### How to get started
Please follow the following [guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/deploy-and-run-raspberrypi-stream.md)
 to set up the infrastructure with Azure Resource Management Templates and start streaming IoT data from a Raspberry Pi

## Contribute
See anything you want to improve? Do you want to build out the existing code base? Don't heistate to open a PR!
Please read our [contributing guidelines] (https://github.com/excellalabs/azure-stream-analysis/blob/master/contributing.md)


