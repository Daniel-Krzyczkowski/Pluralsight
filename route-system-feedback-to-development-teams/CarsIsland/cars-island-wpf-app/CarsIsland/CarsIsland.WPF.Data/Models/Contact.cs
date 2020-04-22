using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WPF.Data.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Location Location { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + "\nPhone number: " + PhoneNumber + "\nE-mail address: " + Email;
        }
    }
}
