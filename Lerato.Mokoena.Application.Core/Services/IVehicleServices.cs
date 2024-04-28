using Lerato.Mokoena.Application.Core.Domain;

namespace Lerato.Mokoena.Application.Core.Services
{
    public interface IVehicleServices
    {
       public void GetNearestVehicle(GeoPosition[] coordinates);

       public void DisplayVehiclePositions(IList<GetVehiclePosition> vehiclePositionList);

       public IList<GetVehiclePosition> GetListOfFilterdCoordinates(IList<GetVehiclePosition> vehiclePositions, GeoPosition[] coords);

       public void GetMinMaxCoordinates(GeoPosition[] coords, out GeoPosition minCoordinate, out GeoPosition maxCoordinate);

       public GetVehiclePosition GetNearest(IList<GetVehiclePosition> vehiclePositions, float latitude, float longitude);
    }
}
