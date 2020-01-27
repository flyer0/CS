using System;
namespace RoutePlaner
{
    public class Link
    {
        public enum TransportModeType {Ship, Rail, Fligh, Road};
        public TransportModeType TransportMode { get; }

        public City FromCity { get; }
        public City ToCity { get; }

        
    }
}
