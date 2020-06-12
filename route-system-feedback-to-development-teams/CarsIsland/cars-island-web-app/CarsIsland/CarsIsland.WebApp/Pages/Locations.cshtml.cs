using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Pages
{
    public class LocationsModel : PageModel
    {
        public IList<Location> LocationsList { get; set; }
        private readonly CarsIslandDbContext _dbContext;

        public LocationsModel(CarsIslandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            LocationsList = await _dbContext.Locations.ToListAsync();
        }
    }
}