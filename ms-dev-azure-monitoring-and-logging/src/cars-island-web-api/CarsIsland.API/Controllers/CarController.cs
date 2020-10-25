using CarsIsland.Core.Entities;
using CarsIsland.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly IDataRepository<Car> _carRepository;

        public CarController(ILogger<CarController> logger,
                             IDataRepository<Car> carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        /// <summary>
        /// Gets list with available cars for rent
        /// </summary>
        /// <returns>
        /// List with available cars for rent
        /// </returns> 
        /// <response code="200">List with cars</response>
        /// <response code="401">Access denied</response>
        /// <response code="404">Cars list not found</response>
        /// <response code="500">Oops! something went wrong</response>
        [ProducesResponseType(typeof(IReadOnlyList<Car>), 200)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars()
        {
            var allCars = await _carRepository.GetAllAsync();
            return Ok(allCars);
        }
    }
}
