using System;

namespace CarsIsland.WebAPI.Core.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public LocationDto Location { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + ", phone: " + PhoneNumber + ", e-mail:" + Email;
        }
    }
}
