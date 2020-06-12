using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Pages
{
    public class ContactsModel : PageModel
    {
        public IList<Contact> ContactsList { get; set; }
        private readonly CarsIslandDbContext _dbContext;

        public ContactsModel(CarsIslandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            ContactsList = await _dbContext.Contacts.ToListAsync();
        }
    }
}