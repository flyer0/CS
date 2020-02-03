using System;
using System.Collections.Generic;
using System.IO;
using static RoutePlaner.Link;

namespace RoutePlaner
{
    public class LinkRepositoryFile : ILinkRepository
    {
        List<Link> links = new List<Link>();

        public LinkRepositoryFile(string filename, CityRepositoryFile cityRepo)
        {
            int lineeno = 0;
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {

                        lineeno++;
                        string line = reader.ReadLine();
                        string[] cols = line.Split('\t'); //Tabulatoren Getrennte Werte \t
                        string fromcity = cols[0];
                        string tocity = cols[1];
                        links.Add(new Link(cityRepo.findCityByName(fromcity), cityRepo.findCityByName(tocity), Link.TransportModeType.Rail));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Invalid syntax on {lineeno}");
                    }
                }
            }
        }
        public int Count { get { return links.Count; } }
        public List<City> FindNeighbours(City c, TransportModeType TransportMode)
        {
            List<City> neighbours = new List<City>();
            foreach (Link l in links.FindAll(l => c.Equals(l.FromCity) && l.TransportMode == TransportMode))
            {
                neighbours.Add(l.ToCity);
            }
            foreach (Link l in links.FindAll(l => c.Equals(l.ToCity) && l.TransportMode == TransportMode))
            {
                neighbours.Add(l.ToCity);
            }

            neighbours.Sort(delegate (City a, City b)
            {
                return 1;
            });

            return neighbours;
        }

        public Link FindLink(City from, City to, Link.TransportModeType tmode)
        {
            return links.Find(l => l.FromCity.Equals(from) && l.ToCity.Equals(to) && l.TransportMode == tmode || l.FromCity.Equals(to) && l.ToCity.Equals(from) && l.TransportMode == tmode);
        }
    }
}