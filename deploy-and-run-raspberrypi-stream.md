## How to get started

### Required items
- Azure Subscription
- Raspberry Pi
- [GrovePi](https://www.dexterindustries.com/grovepi/)

### Azure
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-azure-infrastructure.md) to deploy the Azure Infrastructure.

#### Azure Functions
Please refer to [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy-azure-functions.md) to deploy the required function apps used in this sample.

#### Azure Stream Analytics SQL
Once the Azure Infrastructure has been deployed, open the Twitter Stream Analytics Job and paste in the Azure Stream Analytics query from [here](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/azure-stream-analytics/raspberry-pi-streaming-job)


#### Reference data 
The sensor data stream from your Raspberry Pi will contain individual data points as well as the unique device id for your device. To be able to enrich your stream with additional information, e.g. a friendly device name, please navigate to your storage account and to the referencedata blob container. In this container, please copy in an updated version of the [reference_data.json](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/azure-stream-analytics/reference_data.json)

```
   [ 
     {
        "devideId" : "00000000eb12345",
        "deviceName" : "John's Raspberry PI"
     }]
```

### Raspberry PI
So you're ready to start streaming data from a Raspberry PI to the cloud? Great!
The following guide below outlines the steps neccessary to achieve this.

#### Update packages
Start and log in to your Raspberry Pi, followed by executing the following command to update all and any packages.

```
$ sudo apt-get update
```

#### Install GrovePi
Install the package for GrovevPi's firmware.

```
$ sudo curl -kL dexterindustries.com/update_grovepi | bash
$ sudo reboot
```

You may need to update the firmware for the GrovePi. If so, please connect the GrovePi to your Raspberry PI and run the following commands: 

```
$ cd /home/pi/Dexter/GrovePi/Firmware
$ bash firmware_update.sh
```

#### Install Azure python package
This repo utilized an Azure python package to communicate with an Azure EventHub.

```
$ pip install azure
```

#### Setup sensors
1. Connect your GrovePi to your Raspberry PI
2. Connect the temperature sensor to port digital D7 
3. Connect the sound sensor to analog port A0
4. Connect the light sensor to analog port A1
5. Connect the ultra-sonic range sensor to  digital port D4

![Raspberry PI](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/img/raspberry-pi-setup.jpg)

#### Sends sensor data to Azure
Download [stream_raspberrypi_sensors.py](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/raspberry-pi/stream_raspberrypi_sensors_to_azure.py)

Update the script with configuration data for your Azure Event Hub.
In a command shell, navigate to the directory of the script and run

```
  $ sudo python stream_raspberrypi_sensors_to_azure.py
```

No Raspberry Pi or GrovePi at hand? No worries, follow the following guide to simulate an [event stream](https://github.com/excellalabs/azure-stream-analysis/blob/master/simulate-iot-data.md)

### Power BI
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset
