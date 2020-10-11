using CarsIsland.Core.Entities;
using CarsIsland.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CarsIsland.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars()
        {
            var allCars = await _carRepository.GetAllAsync();

            return Ok(allCars);
        }
    }
}
