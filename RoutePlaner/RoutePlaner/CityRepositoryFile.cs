using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RoutePlaner
{
    public class CityRepositoryFile : ICityRepository
    {
        public List<City> cities = new List<City>();
        public CityRepositoryFile(string filename)
        {

            using (StreamReader reader = new StreamReader(filename))
            {

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] cols = line.Split('\t');
                    string name = cols[0];
                    string country = cols[1];
                    int population = int.Parse(cols[2]);
                    // CultureInfo.InvariantCulture Prevent decimal separator errors (. or ,)
                    double lat = double.Parse(cols[3], CultureInfo.InvariantCulture);
                    double lon = double.Parse(cols[4], CultureInfo.InvariantCulture);

                    City city = new City(name, country, population, new WayPoint(name, lat, lon));
                    cities.Add(city);

                }
            }

        }

        // Get Number of Cities
        public int Count() { return cities.Count; }

        // Find Cities Between specified range (Used for Route Manager algorithm)
        public List<City> FindCitiesBetween(City source, City target)
        {
            // Define range of search
            double minlat = Math.Min(source.Location.Latitude, target.Location.Latitude);
            double minlon = Math.Min(source.Location.Longitude, target.Location.Longitude);
            double maxlat = Math.Max(source.Location.Latitude, target.Location.Latitude);
            double maxlon = Math.Max(source.Location.Longitude, target.Location.Longitude);

            double delta = Math.Max(0.5 * (maxlat - minlat), 0.5 * (maxlon - minlon));

            return cities.FindAll(c => c.Location.Latitude > minlat - delta &&
            c.Location.Latitude > minlat - delta &&
            c.Location.Longitude > minlon - delta &&
            c.Location.Latitude < maxlat - delta &&
            c.Location.Longitude < maxlon - delta);

           
        }

        // Find City by Name
        public City findCityByName(string name)
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
            // List<City> neighbours = new List<City>();


            //neighbours = cities.FindAll(c => loc.Distance(c.Location) < distance);

            /*
            foreach (City c in cities)
            {
                if (loc.Distance(c.Location) < distance)
                {
                    neighbours.Add(c);
                }
            }*/


            //Lambda

            List<City> neighbours = cities.FindAll(c => loc.Distance(c.Location) < distance);
            neighbours.Sort(
                delegate (City a, City b)
                {
                    if (loc.Distance(a.Location) < loc.Distance(b.Location))
                    {
                        return -1;
                    }
                    return 1;
                });
            return neighbours;
        }

    }
}
