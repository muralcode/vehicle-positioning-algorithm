using Lerato.Mokoena.CompositionRoot.Config;
using System.Text;

namespace Lerato.Mokoena.Application.Core.Domain
{
    public class GetVehiclePosition
    {
        public int positionId;
        public string? Registration;
        public float Latitude;
        public float Longitude;
        public DateTime RecordedTimeUTC;

        internal byte[] GetBytes()
        {
            List<byte> byteList = new List<byte>();
            byteList.AddRange(BitConverter.GetBytes(positionId));
            byteList.AddRange(Config.ToNullTerminatedString(Registration));
            byteList.AddRange(BitConverter.GetBytes(Latitude));
            byteList.AddRange(BitConverter.GetBytes(Longitude));
            byteList.AddRange(BitConverter.GetBytes(Config.ToCTime(RecordedTimeUTC)));
            return byteList.ToArray();
        }

        internal string GetTextSummary() => string.Format("{0}, {1}, {2}, {3}, {4}", positionId, Registration, RecordedTimeUTC, Latitude, Longitude);

        public static GetVehiclePosition ReadFromBytes(byte[] buffer, ref int offset)
        {
            GetVehiclePosition vehiclePosition = new GetVehiclePosition();
            vehiclePosition.positionId = BitConverter.ToInt32(buffer, offset);
            offset += 4;

            StringBuilder stringBuilder = new StringBuilder();
            while (buffer[offset] != 0)
            {
                stringBuilder.Append((char)buffer[offset]);
                ++offset;
            }

            vehiclePosition.Registration = stringBuilder.ToString();
            ++offset;

            vehiclePosition.Latitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;

            vehiclePosition.Longitude = BitConverter.ToSingle(buffer, offset);
            offset += 4;

            ulong uint64 = BitConverter.ToUInt64(buffer, offset);
            vehiclePosition.RecordedTimeUTC = Config.FromCTime(uint64);
            offset += 8;

            return vehiclePosition;
        }
    }
}
