namespace RoutePlaner
{
   public class Link
    {
        public enum TransportModeType { Ship, Rail, Flight, Road };

        public City FromCity { get; }
        public City ToCity { get; }
        public TransportModeType TransportMode { get; }
        public double Distance { get { return FromCity.Distance(ToCity); } }

        public Link(City FromCity, City ToCity, TransportModeType TransportMode)
        {
            this.FromCity = FromCity;
            this.ToCity = ToCity;
            this.TransportMode = TransportMode;
        }

        public override string ToString()
        {
            return $"{FromCity.Name} -> {ToCity.Name} : {Distance} via {TransportMode}";
        }
    }
}