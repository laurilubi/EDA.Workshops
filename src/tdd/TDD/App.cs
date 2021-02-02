using System.Collections.Generic;
using System.Linq;

namespace TDD
{
    public class App
    {
        public List<IntegrationEvent> History;
        public EventPublisher EventPublisher;
        public State State;

        public App(IEnumerable<Package> factoryPackages)
        {
            History = new List<IntegrationEvent>();
            EventPublisher = new EventPublisher(History);
            
            State = new State();
            State.Hour = 0;
            State.FactoryPackages = factoryPackages.ToList();
            State.PortPackages = new List<Package>();
            State.Vehicles = new List<Vehicle>
            {
                new Truck(1),
                new Truck(2),
                new Ship(3),
            };
        }

        public void HandleHour()
        {
            State.Vehicles.ForEach(_ => _.HandleLoad(State));
            State.Vehicles.ForEach(_ => _.HandleMove(State));
            State.Vehicles.ForEach(_ => EventPublisher.HandleDepart(_, State) );
            
            State.Hour++;
            
            State.Vehicles.ForEach(_ => EventPublisher.HandleArrive(_, State) );
            State.Vehicles.ForEach(_ => _.HandleUnload(State));

            if (State.FactoryPackages.Any()) return;
            if (State.PortPackages.Any()) return;
            if (State.Vehicles.Any(_ => _.Package != null)) return;
            State.IsDone = true;
        }
    }

    public class State
    {
        public int Hour;
        public List<Package> FactoryPackages;
        public List<Package> PortPackages;
        public List<Vehicle> Vehicles;
        public bool IsDone;
    }
}