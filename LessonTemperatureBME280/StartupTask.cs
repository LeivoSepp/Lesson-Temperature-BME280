using System;
using Windows.ApplicationModel.Background;
using Glovebox.Graphics.Drivers;
using Glovebox.Graphics.Components;
using Glovebox.IoT.Devices.Sensors;
using System.Threading.Tasks;

namespace LessonTemperatureBME280
{
    public sealed class StartupTask : IBackgroundTask
    {
        Ht16K33 driver = new Ht16K33(new byte[] { 0x70 }, Ht16K33.Rotate.None);
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            LED8x8Matrix matrix = new LED8x8Matrix(driver);
            BME280 tempPressureAndHumidity = new BME280(0x76);

            while (true)
            {
                var message = $"{Math.Round(tempPressureAndHumidity.Temperature.DegreesCelsius, 1)}C, {Math.Round(tempPressureAndHumidity.Pressure.Atmospheres, 1)}atm, {Math.Round(tempPressureAndHumidity.Humidity, 1)}% ";
                matrix.ScrollStringInFromRight(message, 70);
                Task.Delay(1000).Wait();
            }
        }
    }
}
