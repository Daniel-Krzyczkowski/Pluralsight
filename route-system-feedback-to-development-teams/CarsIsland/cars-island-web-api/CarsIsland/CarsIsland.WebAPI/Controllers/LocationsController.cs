using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsDataService _locationsDataService;
        public LocationsController(ILocationsDataService locationsDataService)
        {
            _locationsDataService = locationsDataService;
        }

        [HttpGet]
        public async Task<IEnumerable<LocationDto>> Get()
        {
            var locations = await _locationsDataService.GetLocations();
            return locations;
        }
    }
}