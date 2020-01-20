using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RoutePlaner
{
    public class CityRepositoryFile
    {
        private List<City> cities = new List<City>();
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

        // Find City by Name
        public City findCityByName(string name)
        {
            foreach(City c in cities)
            {
                if (c.Name.Equals(name))
                {
                    return c;
                }
            }
            return null;
        }
    }
}
