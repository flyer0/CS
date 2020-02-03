using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutePlaner.Model;
using RoutePlaner.Repository;
using RoutePlaner.Service;

namespace RoutePlaner
{
    class Program
    {
        private void PrintPratteln()
        {
            WayPoint pratteln = new WayPoint("Pratteln", 47.5167, 7.6833);
            Console.WriteLine(pratteln);
        }
        private void PrintWayPointDistance()
        {
            WayPoint bern = new WayPoint("Bern", 46.95, 7.44);
            WayPoint berlin = new WayPoint("Berlin", 52.52, 13.38);
            Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Distance(berlin):####.#} km");
        }
        static void Main(string[] args)
        {
            //WriteLine steht für COUT
            Console.WriteLine("Route Planner 0.0");

            CityRepositoryFile cityRepo = new CityRepositoryFile("cities.txt");

            City berlin = cityRepo.FindCityByName("Berlin");
            City bern = cityRepo.FindCityByName("Bern");

            //City glarus = cityRepo.FindCityByName("Glarus");

            Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Distance(berlin):####.#} km");


            WayPoint pratteln = new WayPoint("Pratteln", 47.5167, 7.6833);

            Console.WriteLine($"Neighbours of {pratteln.Name}");
            foreach (City n in cityRepo.FindNeighbours(pratteln, 60.0))
            {
                Console.WriteLine($"{n.Name:20}: {pratteln.Distance(n.Location): ###.#} km ");
            }

            LinkRepositoryFile linkRepo = new LinkRepositoryFile("links.txt", cityRepo);
            Console.WriteLine($"Links {linkRepo.Count}");

            Console.WriteLine("Links from Basel to Liestal:");
            City Liestal = cityRepo.FindCityByName("Liestal");
            City Basel = cityRepo.FindCityByName("Basel");

            Console.WriteLine(linkRepo.FindLink(Basel, Liestal, Link.TransportModeType.Rail));

            string a = "Basel";
            string b = "Berlin";

            Console.WriteLine($"Shortest Route between {a} and {b}", a, b);
            RouteManager routeManager = new RouteManager(cityRepo, linkRepo);
            foreach (Link l in routeManager.FindShortestRouteBetween(a, b, Link.TransportModeType.Rail))
            {
                Console.WriteLine(l);
            }
            
            //ReadKey oder ReadLine ist sowas wie CIN
            Console.ReadKey();
        }
    }
}
