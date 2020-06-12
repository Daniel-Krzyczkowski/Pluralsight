using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsIsland.WebApp.Data.Models
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }
        public string ImageUrl { get; set; }

        public string Cost { get; set; }
        public Contact ContactPerson { get; set; }
        public Location Location { get; set; }
    }
}
