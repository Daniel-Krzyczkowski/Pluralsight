using AutoMapper;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using CarsIsland.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Services
{
    public class LocationsDataService : BaseDataService, ILocationsDataService
    {
        public LocationsDataService(CarsIslandDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<LocationDto>> GetLocations()
        {
            var locations = await _dbContext.Locations
                             .Select(l => _mapper.Map<LocationDto>(l))
                             .ToListAsync();
            return locations;
        }
    }
}
