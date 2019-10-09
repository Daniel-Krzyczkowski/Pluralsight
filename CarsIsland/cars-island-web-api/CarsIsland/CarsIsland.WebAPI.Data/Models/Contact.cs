using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Data.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }

        public virtual Guid LocationId { get; set; }
    }
}
