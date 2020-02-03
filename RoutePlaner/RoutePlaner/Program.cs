using System;
using RoutePlanner;

namespace RoutePlaner
{
    class MainClass
    {

        //OLD
        private void PrintPratteln()
        {
            WayPoint pratteln = new WayPoint("Pratteln", 47.5167, 7.6833);
            
        }

        //OLD
        private void PrintWayPointDistance()
        {
            WayPoint bern = new WayPoint("Bern", 46.95, 7.44);

            WayPoint berlin = new WayPoint("Berlin", 52.52, 13.38);

            Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Distance(berlin):####.#} km");
        }

        public static void Main(string[] args)
        {
            

            CityRepositoryFile cityRepo = new CityRepositoryFile("cities.txt");

            

            Console.WriteLine(cityRepo.findCityByName("Basel"));

            City bern = cityRepo.findCityByName("Bern");
            City berlin = cityRepo.findCityByName("Berlin");

            // Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Location.Distance(berlin.Location):####.#} km");
            Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Distance(berlin):####.#} km");

            WayPoint pratteln = new WayPoint("Pratteln", 47.5167, 7.6833);

            foreach (City c in cityRepo.FindNeighbours(pratteln, 60.0))
            {
                Console.WriteLine($"{c.Name}: {pratteln.Distance(c.Location):###.#} km");
            }

            LinkRepositoryFile linkRepo = new LinkRepositoryFile("links.txt",cityRepo);
            City Basel = cityRepo.findCityByName("Basel");
            Console.WriteLine($"Neighbors of {Basel.Name}");
            foreach(City n in linkRepo.FindNeighbours(Basel, Link.TransportModeType.Rail))
            {
                Console.WriteLine(n.Name);
            }
            Console.WriteLine("Links from Basel to Liestal:");
            City liestal = cityRepo.findCityByName("Liestal");
            
            Console.WriteLine(linkRepo.FindLink(Basel, liestal, Link.TransportModeType.Rail));


            string a = "Basel";
            string b = "Berlin";
            Console.WriteLine($"Shortest Route between {a} and {b}, a, b");
            RouteManager routeManager = new RouteManager (cityRepo, linkRepo );
            foreach(Link l in routeManager.FindShortestRouteBetween(a, b, Link.TransportModeType.Rail))
            {
                Console.WriteLine(l);
            }

           // Console.WriteLine($"Link {linkRepo.Count}");
            
            Console.ReadKey();
        }
    }
}
