using Lerato.Mokoena.Application.Core.Domain;
using Lerato.Mokoena.Application.Core.Services;
using Lerato.Mokoena.Application.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lerato.Mokoena.VehiclePosition 
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vehicle Positioning Algorithm!");
           
            var serviceProvider = new ServiceCollection()
                      .AddSingleton<IVehicleServices, VehicleService>()
                      .BuildServiceProvider();

            var getVehicleService = serviceProvider.GetService<IVehicleServices>();

            getVehicleService?.GetNearestVehicle(GetPositionCoordinates());
        }

        private static GeoPosition[] GetPositionCoordinates()
        {
            return new GeoPosition[]
            {
                new GeoPosition { Latitude = 34.544909f, Longitude = -102.100843f },
                new GeoPosition { Latitude = 32.345544f, Longitude = -99.123124f },
                new GeoPosition { Latitude = 33.234235f, Longitude = -100.214124f },
                new GeoPosition { Latitude = 35.195739f, Longitude = -95.348899f },
                new GeoPosition { Latitude = 31.895839f, Longitude = -97.789573f },
                new GeoPosition { Latitude = 32.895839f, Longitude = -101.789573f },
                new GeoPosition { Latitude = 34.115839f, Longitude = -100.225732f },
                new GeoPosition { Latitude = 32.335839f, Longitude = -99.992232f },
                new GeoPosition { Latitude = 33.535339f, Longitude = -94.792232f },
                new GeoPosition { Latitude = 32.234235f, Longitude = -100.222222f }
            };
        }
    }
}