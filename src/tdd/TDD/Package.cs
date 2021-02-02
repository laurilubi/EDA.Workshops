namespace TDD
{
    public class Package
    {
        public int Id { get; }
        public Location Origin { get; }
        public Location Destination { get; }

        public Package(int id, Location origin, Location destination)
        {
            Id = id;
            Origin = origin;
            Destination = destination;
        }
    }
}