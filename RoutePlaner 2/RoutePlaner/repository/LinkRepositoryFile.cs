using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlaner.Model;
using RoutePlaner.Service;

namespace RoutePlaner.Repository
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
                        links.Add(new Link(cityRepo.FindCityByName(fromcity),
                            cityRepo.FindCityByName(tocity), Link.TransportModeType.Rail));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Invalid syntax on {lineeno}");
                    }
                }
            }
        }
        public int Count { get { return links.Count; } }
        //public List<Link> FindNeighbours(City city, Link.TransportModeType transportMode)
        public IEnumerable<City> FindNeighbours(City city, Link.TransportModeType transportMode)
        {
            /*with Lambda
            List<City> neighbours = new List<City>();
            foreach (Link l in links.FindAll(l => city.Equals(l.FromCity) && l.TransportMode == transportMode))
            {
                neighbours.Add(l.ToCity);
            }
            foreach (Link l in links.FindAll(l => city.Equals(l.ToCity) && l.TransportMode == transportMode))
            {
                neighbours.Add(l.FromCity);
            }

            return neighbours;
            */
            //return links.Where(w => w.FromCity.Name == city.Name && transportMode == w.TransportMode).ToList();

            return (from l in links
                    where l.FromCity.Equals(city) && l.TransportMode == Link.TransportModeType.Rail
                    select l.ToCity).Union(from l in links
                                           where l.ToCity.Equals(city) && l.TransportMode == Link.TransportModeType.Rail
                                           select l.FromCity);
        }
        public Link FindLink(City from, City to, Link.TransportModeType tmode)
        {
            return links.Find(l =>
            l.FromCity.Equals(from) && l.ToCity.Equals(to) && l.TransportMode == tmode ||
            l.FromCity.Equals(to) && l.ToCity.Equals(from) && l.TransportMode == tmode);
        }
    }
}
