using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RoutePlaner;

namespace RoutePlanner
{
    public class RouteManager
    {
        ICityRepository cityRepository;
        ILinkRepository linkRepository;

        public RouteManager(
            ICityRepository cityRepository,
            ILinkRepository linkRepository)
        {
            this.cityRepository = cityRepository;
            this.linkRepository = linkRepository;
        }
        /** find shortest path between source to target city
         * {@link http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm}
         * 
         * @return itinerary
          */
        public Link[] FindShortestRouteBetween(string fromName, String toName,
                                           Link.TransportModeType tmode)
        {
            City source = cityRepository.findCityByName(fromName);
            City target = cityRepository.findCityByName(toName);
            List<City> cities = cityRepository.FindCitiesBetween(
                source, target);
            if (cities.Count < 1)
                return null;

            List<City> Q = new List<City>();
            Dictionary<City, Double> dist = new Dictionary<City, Double>();
            Dictionary<City, City> previous = new Dictionary<City, City>();
            foreach (City v in cities)
            {
                dist[v] = double.MaxValue;
                previous[v] = null;
                Q.Add(v);
            }
            dist[source] = 0.0;
            while (Q.Count > 0)
            {
                City u = null;
                double minDist = double.MaxValue;
                // find city u with smallest dist
                foreach (City c in Q)
                {
                    if (dist[c] < minDist)
                    {
                        u = c;
                        minDist = dist[c];
                    }
                }
                if (u == null) break;
                Q.Remove(u);
                foreach (City n in linkRepository.FindNeighbours(u, tmode))
                {
                    Link l = linkRepository.FindLink(u, n, tmode);
                    double d = dist[u];
                    if (l != null) d += l.Distance;
                    else d += double.MaxValue;
                    if (dist.ContainsKey(n) && d < dist[n])
                    {
                        dist[n] = d;
                        previous[n] = u;
                    }
                }

            }
            return createItinerary(source, target, previous);
        }

        /* create itinary from source to city */
        private Link[] createItinerary(City source, City target,
                                       Dictionary<City, City> connections)
        {
            List<City> citiesOnRoute = new List<City>();
            City cr = target;
            while (connections[cr] != null)
            {
                citiesOnRoute.Add(cr);
                cr = connections[cr];
            }
            citiesOnRoute.Add(source);

            citiesOnRoute.Reverse();
            City from = null;
            City to = null;
            List<Link> itinerary = new List<Link>();
            foreach (City c in citiesOnRoute)
            {
                from = to;
                to = c;
                if (from != null)
                {
                    itinerary.Add(new Link(from, to, Link.TransportModeType.Rail));
                }
            }
            return itinerary.ToArray();

        }

    }
}