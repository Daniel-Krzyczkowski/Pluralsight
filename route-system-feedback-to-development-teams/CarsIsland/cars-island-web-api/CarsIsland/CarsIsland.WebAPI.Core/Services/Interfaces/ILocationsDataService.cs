using CarsIsland.WebAPI.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Services.Interfaces
{
    public interface ILocationsDataService
    {
        Task<IList<LocationDto>> GetLocations();
    }
}
