using System.Collections.Generic;
using Newtonsoft.Json;

namespace TDD
{
    public class IntegrationEvent
    {
        [JsonProperty(PropertyName = "event")] public TypeEnum Type { get; set; }
        [JsonProperty(PropertyName = "time")] public int Hour { get; set; }

        [JsonProperty(PropertyName = "transport_id")]
        public int TransportId { get; set; }

        [JsonProperty(PropertyName = "kind")] public KindEnum Kind { get; set; }

        [JsonProperty(PropertyName = "location")]
        public LocationEnum Location { get; set; }

        [JsonProperty(PropertyName = "destination")]
        public LocationEnum? Destination { get; set; } // vehicle destination

        [JsonProperty(PropertyName = "cargo")] public List<CargoClass> Cargos { get; set; } = new List<CargoClass>();

        public class CargoClass
        {
            [JsonProperty(PropertyName = "cargo_id")]
            public int CargoId { get; set; }

            [JsonProperty(PropertyName = "destination")]
            public LocationEnum FinalDestination { get; set; }

            [JsonProperty(PropertyName = "origin")]
            public LocationEnum Origin { get; set; }
        }

        public enum TypeEnum
        {
            DEPART,
            ARRIVE
        }

        public enum KindEnum
        {
            TRUCK,
            SHIP
        }

        public enum LocationEnum
        {
            FACTORY,
            PORT,
            A,
            B,
        }
    }
}