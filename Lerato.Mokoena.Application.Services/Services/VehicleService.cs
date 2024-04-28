using Lerato.Mokoena.Application.Core.Domain;
using Lerato.Mokoena.Application.Core.Services;
using Lerato.Mokoena.Application.Services.Facades;
using Lerato.Mokoena.CompositionRoot.Config;
using System.Diagnostics;

namespace Lerato.Mokoena.Application.Services.Services
{
    public sealed class VehicleService : IVehicleServices
    {
        public void GetNearestVehicle(GeoPosition[] coordinates) 
        {
            List<GetVehiclePosition> vehiclePositionList = new List<GetVehiclePosition>();

            Stopwatch stopwatch = Stopwatch.StartNew();

            List<GetVehiclePosition> vehiclePositions = BinaryFileFacade.ReadBinaryFile();

            var listOfFilterdVehiclePositions = GetListOfFilterdCoordinates(vehiclePositions, coordinates);

            stopwatch.Stop();

            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();

            Parallel.ForEach(coordinates, coord =>
            {
                vehiclePositionList.Add(GetNearest(listOfFilterdVehiclePositions, coord.Latitude, coord.Longitude));
            });

            stopwatch.Stop();

            Console.WriteLine($"Binary file read execution time : {elapsedMilliseconds} ms");
            Console.WriteLine($"Closest position calculation execution time : {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total execution time : {(elapsedMilliseconds + stopwatch.ElapsedMilliseconds)} ms");

            if (vehiclePositionList.Count > 0)
            {
                DisplayVehiclePositions(vehiclePositionList);
            }

            Console.WriteLine();
        }

        public void DisplayVehiclePositions(IList<GetVehiclePosition> vehiclePositionList)
        {
            Console.WriteLine($"The nearest vehicle position of the 10 coordinates provided: \n");

            foreach (var vehicle in vehiclePositionList)
            {
                Console.WriteLine($"ID: {vehicle.positionId} Registration: {vehicle.Registration} Latitude: {vehicle.Latitude} Longitude: {vehicle.Longitude} RecordedTimeUTC: {vehicle.RecordedTimeUTC}");
            }
        }
        public IList<GetVehiclePosition> GetListOfFilterdCoordinates(IList<GetVehiclePosition> vehiclePositions, GeoPosition[] coords)
        {
            GeoPosition minCoordinate, maxCoordinate;

            GetMinMaxCoordinates(coords, out minCoordinate, out maxCoordinate);

            return (from pos in vehiclePositions
                    where (pos.Latitude >= minCoordinate.Latitude && pos.Latitude <= maxCoordinate.Latitude) &&
                          (pos.Longitude >= minCoordinate.Longitude && pos.Longitude <= maxCoordinate.Longitude)
                    select pos).ToList();
        }

        public void GetMinMaxCoordinates(GeoPosition[] coords, out GeoPosition minCoordinate, out GeoPosition maxCoordinate)
        {
            minCoordinate = new GeoPosition()
            {
                Latitude = coords.Min(pos => pos.Latitude),
                Longitude = coords.Min(pos => pos.Longitude)
            };
            maxCoordinate = new GeoPosition()
            {
                Latitude = coords.Max(pos => pos.Latitude),
                Longitude = coords.Max(pos => pos.Longitude)
            };
        }

        public GetVehiclePosition GetNearest(IList<GetVehiclePosition> vehiclePositions, float latitude, float longitude)
        {
            double nearestDistance = double.MaxValue;
            GetVehiclePosition? nearest = null;

            Parallel.ForEach(vehiclePositions, vehiclePosition =>
            {
                double num = Config.DistanceBetween(latitude, longitude, vehiclePosition.Latitude, vehiclePosition.Longitude);
                if (num < nearestDistance)
                {
                    nearest = vehiclePosition;
                    nearestDistance = num;
                }
            });

            return nearest;
        }
    }
}
