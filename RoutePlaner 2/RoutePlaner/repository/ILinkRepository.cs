using System.Collections.Generic;
using RoutePlaner.Model;
using RoutePlaner.Service;

namespace RoutePlaner.Repository
{
    public interface ILinkRepository
    {
        int Count { get; }

        Link FindLink(City from, City to, Link.TransportModeType tmode);
        IEnumerable<City> FindNeighbours(City city, Link.TransportModeType transportMode);
    }
}