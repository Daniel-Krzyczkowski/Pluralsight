using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Pages
{
    public class CarDetailsModel : PageModel
    {
        private readonly CarsIslandDbContext _dbContext;

        public Car FoundCar { get; set; }
        public Contact ContactPerson { get; set; }

        public CarDetailsModel(CarsIslandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet(Guid id)
        {
            var carId = id;
            FoundCar = await _dbContext.Cars
                                        .Include(c => c.ContactPerson)
                                        .Include(c => c.Location)
                                        .FirstOrDefaultAsync(c => c.Id == carId);
            if (FoundCar != null)
            {
                ContactPerson = await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == FoundCar.ContactPerson.Id);
            }
        }
    }
}