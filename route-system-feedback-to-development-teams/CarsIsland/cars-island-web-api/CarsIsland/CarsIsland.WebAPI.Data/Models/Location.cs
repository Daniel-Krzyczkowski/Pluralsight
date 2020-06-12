using System;

namespace CarsIsland.WebAPI.Data.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
