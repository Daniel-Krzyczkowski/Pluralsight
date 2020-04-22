using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Cost { get; set; }

        public string ImageUrl { get; set; }

        public Contact ContactPerson { get; set; }

        public Location Location { get; set; }
    }
}
