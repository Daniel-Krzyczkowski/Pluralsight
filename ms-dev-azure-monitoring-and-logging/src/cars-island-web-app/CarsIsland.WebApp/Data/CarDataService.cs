using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Data
{
    public class CarDataService
    {
        public Task<Car[]> GetCarsAsync()
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new Car
            {
                Brand = "BMW",
                Model = "320",
                Availability = 3,
                PricePerDay = 120,
                ImageUrl = "https://image.freepik.com/free-vector/bmw-white-car-icon-vector_91-8564.jpg"
            }).ToArray());
        }
    }
}
