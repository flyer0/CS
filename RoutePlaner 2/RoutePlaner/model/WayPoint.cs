using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlaner.Repository;
using RoutePlaner.Service;

namespace RoutePlaner.Model
{
    public class WayPoint
    {   //Automatic properties
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public WayPoint(string name, Double lat, Double lon)
        {
            Name = name;
            Latitude = lat;
            Longitude = lon;
        }
        public double Distance(WayPoint other)
        {
            double R = 6371.0; //Radius in KM
            double phia = Latitude * Math.PI / 180; //Radiant (Mathematische Winkeleinheit)
            double phib = other.Latitude * Math.PI / 180;
            double lama = Longitude * Math.PI / 180;
            double lamb = other.Longitude * Math.PI / 180;
            return R * Math.Acos(Math.Sin(phia) * Math.Sin(phib) + Math.Cos(phia) * Math.Cos(phib) * Math.Cos(lama - lamb));
        }
        public override string ToString()
        {
            double latmin = (Latitude -(int)Latitude) * 60; //Latitude in Minuten
            double lonmin = (Longitude - (int)Longitude) * 60; //Longitude in Minuten

            return
                $"WayPoint: {Name} {Latitude:##}°{latmin:##}'/{Longitude:##}°{lonmin:##}'";
            //Ausgabe
        }
       
    }
}
