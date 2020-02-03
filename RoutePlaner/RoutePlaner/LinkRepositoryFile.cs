using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static RoutePlaner.Link;

namespace RoutePlaner
{
    public class LinkRepositoryFile
    {
        private List<Link> links = new List<Link>();

        public LinkRepositoryFile(string filename, CityRepositoryFile cityRepo)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                int linenr = 0;
                while (!reader.EndOfStream)
                {
                    try
                    {
                        linenr += 1;
                        string line = reader.ReadLine();
                        string[] cols = line.Split('\t');
                        string fromCity = cols[0];
                        string toCity = cols[1];
                        links.Add(new Link(cityRepo.findCityByName(fromCity), cityRepo.findCityByName(toCity), Link.TransportModeType.Rail));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Invalid syntax on {linenr}");
                    }

                }
            }
        }

        public int Count { get { return links.Count; } }

        private List<Link> findNeighbours(City city, TransportModeType transportMode)
        {
            return links.Where(w => w.FromCity.Name == city.Name && transportMode == w.TransportMode).ToList();
        }
    }
   
}
