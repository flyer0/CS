using System;
namespace RoutePlaner
{
    public class City
    {
        public string Name { get; }
        public string Country { get; }
        public int Population { get; }
        public WayPoint Location { get; }
        
        public City(string name, string country, int population, WayPoint loc)
        {
            Name = name;
            Country = country;
            Population = population;
            Location = loc;
        }

        public override string ToString()
        {
            return $"City population {Population} {Location}";
        }

        public double Distance(City other)
        {
            return Location.Distance(other.Location);
        }
    }
}
