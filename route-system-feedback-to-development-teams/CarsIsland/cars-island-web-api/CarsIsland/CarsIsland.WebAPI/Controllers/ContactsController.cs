using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsIsland.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsDataService _contactsDataService;
        public ContactsController(IContactsDataService contactsDataService)
        {
            _contactsDataService = contactsDataService;
        }

        [HttpGet]
        public async Task<IEnumerable<ContactDto>> Get()
        {
            var contacts = await _contactsDataService.GetContacts();
            return contacts;
        }
    }
}