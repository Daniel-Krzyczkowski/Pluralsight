using CarsIsland.WPF.Core.Config;
using CarsIsland.WPF.Core.Services.Interfaces;
using CarsIsland.WPF.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WPF.Core.Services
{
    public class CarsDataService : BaseDataService, ICarsDataService
    {
        public async Task<IList<Car>> GetAvailableCars()
        {
            var response = await _httpClient.GetAsync(AppConfiguration.CarsDataEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();
            var cars = JsonConvert.DeserializeObject<IList<Car>>(responseContent);
            return cars;
        }
    }
}
