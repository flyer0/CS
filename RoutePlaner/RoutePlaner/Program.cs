using System;

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
            Console.WriteLine($"Link {linkRepo.Count}");
            
            Console.ReadKey();
        }
    }
}
