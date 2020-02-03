using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RoutePlaner
{
    class Link
    {
        public enum TransportModeType { Ship, Rail, Flight, Road };
        public City FromCity { get; }
        public City ToCity { get; }
        public double Distance { get { return FromCity.Distance(ToCity); } }
        public TransportModeType TransportMode { get; }
        public Link(City fromCity, City toCity, TransportModeType transportMode)
        {
            FromCity = fromCity;
            ToCity = toCity;
            TransportMode = transportMode;
        }
        public override string ToString()
        {
            return $"{FromCity.Name,-20} {ToCity.Name,-20} {FromCity.Distance(ToCity):####.#) km}";
        }
    }
}
