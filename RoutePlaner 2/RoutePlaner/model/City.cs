using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlaner.Repository;
using RoutePlaner.Service;

namespace RoutePlaner.Model
{
    public class City
    {
        public string Name { get; }
        public string Country { get; }
        public int Population { get; }
        public WayPoint Location { get; }
        public double Distance(City other)
        {
            return Location.Distance(other.Location);
        }
        public City(string name, string country, int population, WayPoint location)
        {
            Name = name;
            Country = country;
            Population = population;
            Location = location;
        }

        public override string ToString()
        {
                return $"City population {Population} {Location}";

        }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   Name == city.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
