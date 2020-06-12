using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Pages
{
    public class CarsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public List<Car> CarsList { get; set; }
        private readonly CarsIslandDbContext _dbContext;

        public CarsModel(CarsIslandDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                CarsList = CarsList.Where(c => c.Brand.Contains(SearchString,
                                            StringComparison.InvariantCultureIgnoreCase))
                                            .ToList();
            }

            else
            {
                CarsList = await _dbContext.Cars
                                            .Include(c => c.Location)
                                            .ToListAsync();
            }
        }
    }
}