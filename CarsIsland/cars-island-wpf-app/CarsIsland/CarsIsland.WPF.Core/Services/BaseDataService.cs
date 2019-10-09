using CarsIsland.WPF.Core.Config;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CarsIsland.WPF.Core.Services
{
    public class BaseDataService
    {
        protected readonly HttpClient _httpClient;

        public BaseDataService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(AppConfiguration.CarsIslandApiUrl)
            };
        }
    }
}
