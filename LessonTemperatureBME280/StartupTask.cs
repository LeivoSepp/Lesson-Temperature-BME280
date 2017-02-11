using System;
using Windows.ApplicationModel.Background;
using Glovebox.IoT.Devices.Sensors;
using System.Threading.Tasks;

namespace LessonTemperatureBME280
{
    public sealed class StartupTask : IBackgroundTask
    {
        BME280 tempPressureAndHumidity = new BME280(0x76);
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
