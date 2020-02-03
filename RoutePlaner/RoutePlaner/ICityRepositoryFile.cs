using System.Collections.Generic;

namespace RoutePlaner
{
    public interface ICityRepository
    {
        int Count();
        City findCityByName(string name);
        List<City> FindNeighbours(WayPoint loc, double distance);
        List<City> FindCitiesBetween(City source, City target);
    }
}