using Lerato.Mokoena.Application.Core.Domain;
using Lerato.Mokoena.CompositionRoot.Config;

namespace Lerato.Mokoena.Application.Services.Facades
{
    internal static class BinaryFileFacade
    {
        internal static List<GetVehiclePosition> ReadBinaryFile()
        {
            byte[] data = ReadFile();
            List<GetVehiclePosition> vehiclePositionList = new List<GetVehiclePosition>();
            int offset = 0;
            while (offset < data.Length)
                vehiclePositionList.Add(ReadVehiclePosition(data, ref offset));
            return vehiclePositionList;
        }

        internal static byte[] ReadFile()
        {
            string filePath = Config.GetLocalFilePath("VehiclePositions.dat");
            if (File.Exists(filePath))  
                return File.ReadAllBytes(filePath);
            Console.WriteLine(" Binary Data file not found!!!");
            return null;
        }

        private static GetVehiclePosition ReadVehiclePosition(byte[] data, ref int offset) 
                       => GetVehiclePosition.ReadFromBytes(data, ref offset);
    }
}
