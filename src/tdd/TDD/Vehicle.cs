using System.Linq;

namespace TDD
{
    public abstract class Vehicle
    {
        public int Pos;
        public Package Package;

        public abstract void HandleLoad(State state);
        public abstract void HandleMove(State state);
        public abstract void HandleUnload(State state);
    }

    public class Truck : Vehicle
    {
        public Truck()
        {
            Pos = 0;
        }

        public override void HandleLoad(State state)
        {
            if (Pos == 0 && Package == null && state.FactoryPackages.Any())
            {
                // load in factory
                Package = state.FactoryPackages.First();
                state.FactoryPackages = state.FactoryPackages.Skip(1).ToList();
            }
        }

        public override void HandleMove(State state)
        {
            if (Package == null && Pos > 0)
                Pos--;
            else if (Package == null && Pos < 0)
                Pos++;
            else if (Package?.Destination == DestinationType.A && Pos < 1)
                Pos++;
            else if (Package?.Destination == DestinationType.B && Pos > -5)
                Pos--;
        }

        public override void HandleUnload(State state)
        {
            if (Pos == 1 && Package != null)
            {
                // unload in port
                state.PortPackages.Add(Package);
                Package = null;
            }

            if (Pos == -5 && Package != null)
            {
                // unload at B
                Package = null;
            }
        }
    }

    public class Ship : Vehicle
    {
        public Ship()
        {
            Pos = 1;
        }

        public override void HandleLoad(State state)
        {
            if (Pos == 1 && Package == null && state.PortPackages.Any())
            {
                // load in port
                Package = state.PortPackages.First();
                state.PortPackages = state.PortPackages.Skip(1).ToList();
            }
        }

        public override void HandleMove(State state)
        {
            if (Package == null && Pos > 1)
                Pos--;
            else if (Package != null && Pos < 5)
                Pos++;
        }

        public override void HandleUnload(State state)
        {
            if (Pos == 5 && Package != null)
            {
                // unload on island
                Package = null;
            }
        }
    }
}