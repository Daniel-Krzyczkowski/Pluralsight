using System;

namespace CarsIsland.WebAPI.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Cost { get; set; }

        public Contact ContactPerson { get; set; }

        public Location Location { get; set; }

        public string ImageUrl { get; set; }

        public virtual Guid ContactPersonId { get; set; }

        public virtual Guid LocationId { get; set; }
    }
}
