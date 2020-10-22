using CarsIsland.WebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Data
{
    public class CarDataService
    {
        private readonly ICarsIslandApiService _carsIslandApiService;

        public CarDataService(ICarsIslandApiService carsIslandApiService)
        {
            _carsIslandApiService = carsIslandApiService;
        }

        public async Task<IReadOnlyCollection<Car>> GetCarsAsync()
        {
            return await _carsIslandApiService.GetAvailableCarsAsync();
        }
    }
}
