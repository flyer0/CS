using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlaner.Model;
using RoutePlaner.Service;

namespace RoutePlaner.Repository
{
    public class CityRepositoryFile : ICityRepository
    {
        private List<City> cities = new List<City>();

        public CityRepositoryFile(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] cols = line.Split('\t'); //Tabulatoren Getrennte Werte \t
                    string name = cols[0];
                    string country = cols[1];
                    int pop = int.Parse(cols[2]); //int.Parse steht für String to Int
                    double lat = double.Parse(cols[3], CultureInfo.InvariantCulture); //CultureInfo wegen Kommastellen Formatierung (. oder ,) 
                    double lon = double.Parse(cols[4], CultureInfo.InvariantCulture);
                    City city = new City(name, country, pop, new WayPoint(name, lat, lon));
                    cities.Add(city);
                }
            }
            //Console.WriteLine($"Cities Total {cities.Count}");
        }
        public int Count { get { return cities.Count; } }
        public City FindCityByName(string name)
        {
            foreach (City c in cities)
            {
                if (c.Name.Equals(name))
                {
                    return c;
                }
            }
            return null;
        }
        public List<City> FindNeighbours(WayPoint loc, double distance)
        {
            List<City> neighbours = new List<City>();
            //mit Lambda Ausdruck statt foreach:
            neighbours = cities.FindAll(c => loc.Distance(c.Location) < distance);

            /*
            foreach (City c in cities)
            {
                if (loc.Distance(c.Location) < distance)
                {
                    neighbours.Add(c);
                }
            }
            */
            neighbours.Sort(delegate (City a, City b)
            {
                if (loc.Distance(a.Location) < loc.Distance(b.Location))
                {
                    return -1;
                }
                return 1;
            });

            return neighbours;
        }
        //Return cities in range for effiency purposes
        public List<City> FindCitiesBetween(City source, City target)
        {
            double minLat = Math.Min(source.Location.Latitude, target.Location.Latitude);
            double minLon = Math.Min(source.Location.Longitude, target.Location.Longitude);
            double maxLat = Math.Max(source.Location.Latitude, target.Location.Latitude);
            double maxLon = Math.Max(source.Location.Longitude, target.Location.Longitude);
            double delta = Math.Max(0.5 * (maxLat - minLat), 0.5 * (maxLon - minLon));

            return cities.FindAll(c =>
            c.Location.Latitude > minLat - delta &&
            c.Location.Latitude < maxLat + delta &&
            c.Location.Longitude > minLon - delta &&
            c.Location.Longitude < maxLon + delta);
        }
    }
}
