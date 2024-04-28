using Lerato.Mokoena.Application.Core.Services;

namespace Lerato.Mokoena.Application.Core.Domain
{
    public struct GeoPosition: IGeoPosition
    { 
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
