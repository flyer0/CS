using System;

namespace RoutePlaner
{
    public class WayPoint
    {
        // Automatic properties
        public double Latitude { get; }
        public double Longitude { get; }
        public string Name { get; private set; } //private is default

        public WayPoint(string name, double lat, double lon)
        {
            Name = name;
            Latitude = lat;
            Longitude = lon;
        }

        public double Distance(WayPoint other)
        {
            double R = 6371.0; // Radius in km
            double phia = Latitude * Math.PI / 180; // Radian
            double phib = other.Latitude * Math.PI / 180;
            double lama = Longitude * Math.PI / 180; // Radian
            double lamb = other.Longitude * Math.PI / 180;

            return R * Math.Acos(Math.Sin(phia) * Math.Sin(phib) + Math.Cos(phia) * Math.Cos(phib) * Math.Cos(lama - lamb));
        }

        public override string ToString()
        {
            double latmin = (Latitude - (int)Latitude) * 60;
            double lomin = (Longitude - (int)Longitude) * 60;
            return $"WayPoint: {Name} {(int)Latitude:##}°{latmin:##} / {(int)Longitude:##}°{lomin:##}";
        }

    }
}
    

