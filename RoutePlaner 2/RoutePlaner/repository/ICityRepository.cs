using System.Collections.Generic;
using RoutePlaner.Model;
using RoutePlaner.Service;

namespace RoutePlaner.Repository
{
    public interface ICityRepository
    {
        int Count { get; }

        City FindCityByName(string name);
        List<City> FindNeighbours(WayPoint loc, double distance);
        List<City> FindCitiesBetween(City source, City target);
    }
}