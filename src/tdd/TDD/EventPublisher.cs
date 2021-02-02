using System;
using System.Collections.Generic;

namespace TDD
{
    public class EventPublisher
    {
        private readonly List<IntegrationEvent> history;

        public EventPublisher(List<IntegrationEvent> history)
        {
            this.history = history;
        }

        public void HandleDepart(Vehicle vehicle, State state)
        {
            var departEvent = GetCommonBase(vehicle.PrevPos, vehicle, state);
            if (departEvent == null) return;

            departEvent.Type = IntegrationEvent.TypeEnum.DEPART;
            departEvent.Destination = GetLocation(vehicle.Destination);

            history.Add(departEvent);
        }

        public void HandleArrive(Vehicle vehicle, State state)
        {
            var arriveEvent = GetCommonBase(vehicle.Pos, vehicle, state);
            if (arriveEvent == null) return;

            arriveEvent.Type = IntegrationEvent.TypeEnum.ARRIVE;

            history.Add(arriveEvent);
        }

        private IntegrationEvent GetCommonBase(int pos, Vehicle vehicle, State state)
        {
            if (vehicle.Pos == vehicle.PrevPos) return null; // has not moved

            var location = LocationExtension.FromPos(pos);
            if (location == null) return null;

            var ev = new IntegrationEvent();
            ev.Hour = state.Hour;
            ev.TransportId = vehicle.Id;
            ev.Kind = vehicle is Truck
                ? IntegrationEvent.KindEnum.TRUCK
                : IntegrationEvent.KindEnum.SHIP;
            ev.Location = GetLocation(location).Value;

            var package = vehicle.Package;
            if (package != null)
            {
                ev.Cargos.Add(new IntegrationEvent.CargoClass
                {
                    CargoId = package.Id,
                    FinalDestination = GetLocation(package.Destination).Value,
                    Origin = GetLocation(package.Origin).Value
                });
            }

            return ev;
        }

        private IntegrationEvent.LocationEnum? GetLocation(Location? location)
        {
            switch (location)
            {
                case null: return null;
                case Location.B: return IntegrationEvent.LocationEnum.B;
                case Location.Factory: return IntegrationEvent.LocationEnum.FACTORY;
                case Location.Port: return IntegrationEvent.LocationEnum.PORT;
                case Location.A: return IntegrationEvent.LocationEnum.A;
                default: throw new NotImplementedException();
            }
        }
    }
}