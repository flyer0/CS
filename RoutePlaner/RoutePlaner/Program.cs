using System;

namespace RoutePlaner
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            WayPoint pratteln = new WayPoint("Pratteln", 47.5167, 7.6833);

            WayPoint bern = new WayPoint("Bern", 46.95, 7.44);

            WayPoint berlin = new WayPoint("Berlin", 52.52, 13.38);

            Console.WriteLine($"Distance {bern.Name} -- {berlin.Name}: " + $"{bern.Distance(berlin):####.#} km");



            CityRepositoryFile cityRepo = new CityRepositoryFile("cities.txt");

            

            Console.WriteLine(cityRepo.findCityByName("Basel"));
            Console.ReadKey();
        }
    }
}
