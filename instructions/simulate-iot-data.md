## How to get started
We totally understand that not everyone has a Raspberry Pi and Grove Pi at hand, to test the complete functionality part of this repo.
For that very specific purpose, we've created a simple .NET Core Console App that sends events to a given event hub in Azure, in the same format as expected by the Azure Stream Analytics job.

To get the IoT simulator working, please do the following:
1. Clone this repository 
2. Open IoTSimulation.sln in prefered IDE
3. Navigate to the appsettings.json and set the following values

| Property | Value |
|----------|-------|
| EventHubPath | Name of Event-Hub Namespace |
| EventHubConnectionString | Connection string to given event hub |

4. Run the console application
