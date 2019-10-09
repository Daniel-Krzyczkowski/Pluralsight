using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Core.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Cost { get; set; }

        public string ImageUrl { get; set; }

        [JsonProperty("ContactPerson")]
        public ContactDto ContactPerson { get; set; }

        [JsonProperty("Location")]
        public LocationDto Location { get; set; }
    }
}
