import uuid
import datetime
import random
import json
import grovepi
from azure.servicebus import ServiceBusService
from grovepi import *
from time import sleep
from math import isnan

#Gets a unique device serial number
def getserial():
  # Extract serial from cpuinfo file
  cpuserial = "0000000000000000"
  try:
    f = open('/proc/cpuinfo','r')
    for line in f:
      if line[0:6]=='Serial':
        cpuserial = line[10:26]
    f.close()
  except:
    cpuserial = "ERROR000000000"

  return cpuserial

#Class holding a data point
class DataPoint(json.JSONEncoder):
	def __init__(self, temp, humidity, soundLevel, lightLevel, distance):
		self.temperature = float(temp)
		self.humidity = float(humidity)
		self.soundLevel = float(soundLevel)
		self.lightLevel = float(lightLevel)
		self.distance = float(distance)
    
# Custom JSON encoder
def encode_data(z):
        return { "DeviceId" : getserial(),
                "SensorReadings" : {
                "Temperature" : z.temperature,
                "Humidity" : z.humidity,
                "SoundLevel" : z.soundLevel,
                "LightLevel" : z.lightLevel,
                "Distance" : z.distance
                }}

#Raspberry PI constants
dht_sensor_port = 7      #Digital D7
dht_sensor_type = 0 
sound_sensor = 0         #Analog A0
light_sensor = 1         #Analog A1
ultrasonic_ranger = 4    #Digital D4

#Azure constants
key_name="RootManageSharedAccessKey"
key_value=""
event_hub_namespace = ""
event_hub_name = ""

sbs = ServiceBusService(service_namespace=event_hub_namespace, shared_access_key_name=key_name, shared_access_key_value=key_value)

while True:
        sleep(0.5)
        soundLevel = grovepi.analogRead(sound_sensor)

        sleep(0.5)
        lightLevel = grovepi.analogRead(light_sensor)

        sleep(0.5)
        distance = ultrasonicRead(ultrasonic_ranger)

        sleep(0.3)
        [ temp,hum ] = dht(dht_sensor_port,dht_sensor_type)

        t = str(temp)
        h = str(hum)

        dataPoint = DataPoint(t, h, soundLevel, lightLevel, distance)
        
        payload = json.dumps(dataPoint, default=encode_data)
        print(payload)

        sbs.send_event(event_hub_name, payload)
        print("sending....")
