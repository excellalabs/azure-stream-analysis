## How to get started

### Prerequisites
- [Azure subscription](https://azure.microsoft.com/en-us/free/)
- [PowerBI](https://powerbi.microsoft.com/en-us/)
- Raspberry Pi
- [GrovePi](https://www.dexterindustries.com/grovepi/)
- [Outlook e-mail](www.outlook.com)

###  Deploy Azure Infrastructure

#### 1. Deploy Azure Resource Management Template
- Navigate to [deploy an ARM template](https://portal.azure.com/#create/Microsoft.Template)
- Click on "Build your own Template in the Editor"
- Copy and paste the [Raspberry PI ARM Template](https://github.com/excellalabs/azure-stream-analysis/blob/master/deploy/azure-stream-analytics--raspberry-pi-template.json)
- Click "Save"

#### 2. Enter valid parameter values
- Select to create a new resource group, or utilize an existing.
- Enter the required template parameters:
    - Notification e-mail
    - Power BI user name
    - Power BI display name

#### 3. Deploy
Select to agree with terms and conditions and click "Purchase" to trigger the deployment.


**Note: The deployment will indicate failure but this is just because it was unable to authenticate the Power BI connection which you will later have to authorize**


#### Authenticate accounts
The ARM template will succesfully set up the required infrastructure but will require you to authenticate you Outlook and PowerBI accounts to fully function.

1. Authenticate Power BI output
- Navigate to your Azure Stream Analytics Job
- Click on the powerbi output
- Click the blue button "Renew Authorization"
- Log-in using your Power BI user account
- Click "Save"
- Navigate back to the Stream Analytics overview page
- Click "Start" to start your streaming analyticsc job

2. Authenticate Outlook notifier
- Navigate to your Azure Logic App
- Click on "Edit"
- Click on the Outlook connection step (last step)
- Click on the invalid connection symbol
- Log-in using your outlook account
- Navigate back to the Logic App overview page
- Click "Enable" to enable your trigger

#### Reference data 
The sensor data stream from your Raspberry Pi will contain individual data points as well as the unique device id for your device. To be able to enrich your stream with additional information, e.g. a friendly device name, please navigate to your storage account and to the reference blob container. In this container, please copy in an updated version of the [reference-data.json](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/azure-stream-analytics/reference-data.json)

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

##### 1. Download python script
Download [stream_raspberrypi_sensors.py](https://github.com/excellalabs/azure-stream-analysis/blob/master/src/raspberry-pi/stream_raspberrypi_sensors_to_azure.py)

##### 2. Update script with configuration parameters
Update the script with configuration data for your Azure Event Hub.

##### 3. Run the script
In a command shell, navigate to the directory of the script and run

```
  $ sudo python stream_raspberrypi_sensors_to_azure.py
```

### Power BI
Please refer to the [following guide](https://github.com/excellalabs/azure-stream-analysis/blob/master/instructions/setting-up-power-bi.md) setting a Power BI dashboard and a streaming dataset
