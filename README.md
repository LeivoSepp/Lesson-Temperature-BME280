# Humidity + Barometric Pressure + Temperature Adafruit BME280
This project is an example on how to use the Adafruit BME280 Humidity + Barometric Pressure + Temperature Sensor in Raspberry PI and Windows 10 IoT Core.

## What is this sensor?
This sensor is great for all sorts of weather/environmental sensing.
Measuring humidity with ±3% accuracy, barometric pressure with ±1 hPa absolute accuraccy, and temperature with ±1.0°C accuracy. 
Because pressure changes with altitude, and the pressure measurements are so good, you can also use it as an altimeter with  ±1 meter or better accuracy!

https://www.adafruit.com/products/2652

![image](https://cloud.githubusercontent.com/assets/13704023/22854277/6efb9342-f073-11e6-9f12-94eb80b05169.png)

Temperature is calculated in degrees C, you can convert this to F by using the classic F = C * 9/5 + 32 equation.

Pressure is returned in the SI units of Pascals. 100 Pascals = 1 hPa = 1 millibar.
You can also calculate Altitude. However, you can only really do a good accurate job of calculating altitude if you know the hPa pressure at sea level for your location and day! The sensor is quite precise but if you do not have the data updated for the current day then it can be difficult to get more accurate than 10 meters.

## How to connect this sensor into Raspberry PI?
To connect this sensor to Raspberry PI you need 4 wires. Two of the wires used for voltage Vin (+3V from Raspberry) and ground GND and remaining two are used for data. 
As this is digital sensor, it uses I2C protocol to communicate with the Raspberry. For I2C we need two wires, Data (SDA) and Clock (SCL).
Connect sensor SDI and SCK pins accordingly to Raspberry SDA and SCL pins. 

![image](https://cloud.githubusercontent.com/assets/13704023/22856394/3b6f5f9c-f099-11e6-8475-6c334e7d0358.png)

## How do I write the code?
You need to add NuGet packages Glovebox.IoT.Devices and UnitsNet to your project and you are almost done :)

After adding these NuGet packages, you just need to write 2 lines of code.

1. Create an object for sensor: 
````C#
        BME280 tempPressureAndHumidity = new BME280();
````

2. Write a while-loop, to read data from the sensor every 1 sec.
````C#
            while (true)
            {
                double temperature = tempPressureAndHumidity.Temperature.DegreesCelsius;
                double pressure = tempPressureAndHumidity.Pressure.Hectopascals; //atm
                double humidity = tempPressureAndHumidity.Humidity;
                Task.Delay(1000).Wait();
            }
````
Final code looks like this. 

If you run it, you do not see anything, because it just reads the data, but it doesnt show it anywhere.
You need to integrate this project with my other example, where I teach how to send this data into Azure.

````C#
using System;
using Windows.ApplicationModel.Background;
using Glovebox.IoT.Devices.Sensors;
using System.Threading.Tasks;

namespace LessonTemperatureBME280
{
    public sealed class StartupTask : IBackgroundTask
    {
        BME280 tempPressureAndHumidity = new BME280();
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            while (true)
            {
                double temperature = tempPressureAndHumidity.Temperature.DegreesCelsius;
                double pressure = tempPressureAndHumidity.Pressure.Hectopascals; //atm
                double humidity = tempPressureAndHumidity.Humidity;
                Task.Delay(1000).Wait();
            }
        }
    }
}
````
## Advanced sensor tuning: change I2C address
I2C address is used to communicate with the sensor. Many sensors have I2C address hardcoded, but this sensor supports two different I2C addresses.
By defult this BME280 sensor uses I2C address 0x77. You can change the address for 0x76 by connecting the sensor pin named SDO to GND (ground). 

1. How to set I2C address to 0x77
   1. Leave the sensor pin addr open
   2. Create a new sensor object without any parameters
````C#
        BME280 tempPressureAndHumidity = new BME280();
````
2. How to set I2C address to 0x76
   1. Connect the sensor pin SDO to GND
   2. Use parameter 0x76 when creating a new sensor object
````C#
        BME280 tempPressureAndHumidity = new BME280(0x76);
````
