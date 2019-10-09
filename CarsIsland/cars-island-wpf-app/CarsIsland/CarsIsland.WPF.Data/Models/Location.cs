using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Data.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }

        public override string ToString()
        {
            return Address + ", " + City + ", " + Country;

        }
    }
}
