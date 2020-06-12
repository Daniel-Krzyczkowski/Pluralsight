using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarsIsland.WebApp.Data.Models
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", phone: " + PhoneNumber + ", e-mail:" + Email;
        }
    }
}
