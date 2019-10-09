using CarsIsland.WPF.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WPF.Core.Services.Interfaces
{
    public interface ILocationsDataService
    {
        Task<IList<Location>> GetLocations();
    }
}
