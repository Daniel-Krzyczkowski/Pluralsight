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
    public class LocationsDataService : BaseDataService, ILocationsDataService
    {
        public async Task<IList<Location>> GetLocations()
        {
            var response = await _httpClient.GetAsync(AppConfiguration.LocationsDataEndpoint);
            var responseContent = await response.Content.ReadAsStringAsync();
            var locations = JsonConvert.DeserializeObject<IList<Location>>(responseContent);
            return locations;
        }
    }
}
