using GeoCoordinatePortable;
using System.Text;

namespace Lerato.Mokoena.CompositionRoot.Config
{
    public static class Config
    {
        internal const string subDirectory = "Binaryfile";

        internal static DateTime Epoch => new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public static string GetLocalFilePath(string fileName) => GetLocalFilePath(subDirectory, fileName);

        public static double DistanceBetween(float latitude1, float longitude1, float latitude2, float longitude2)
        {
            return new GeoCoordinate((double)latitude1, (double)longitude1).GetDistanceTo(new GeoCoordinate((double)latitude2, (double)longitude2));
        }

        public static string GetLocalFilePath(string subDirectory, string fileName) => Path.Combine(GetLocalPath(subDirectory), fileName);

        public static byte[] ToNullTerminatedString(string registration)
        {
            byte[] bytes = Encoding.Default.GetBytes(registration);
            byte[] terminatedString = new byte[bytes.Length + 1];
            bytes.CopyTo((Array)terminatedString, 0);
            return terminatedString;
        }

        public static string GetLocalPath(string subDirectory)
        {
            string path1 = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            if (subDirectory != string.Empty)
                path1 = Path.Combine(path1, subDirectory);
            return path1;
        }

        public static ulong ToCTime(DateTime time) => Convert.ToUInt64((time - Epoch).TotalSeconds);

        public static DateTime FromCTime(ulong cTime) => Epoch.AddSeconds((double)cTime);
    }
}
