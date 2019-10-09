using AutoMapper;
using CarsIsland.WebAPI.Core.DTOs;
using CarsIsland.WebAPI.Core.Services.Interfaces;
using CarsIsland.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Services
{
    public class CarsDataService : BaseDataService, ICarsDataService
    {
        public CarsDataService(CarsIslandDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }

        public async Task<IList<CarDto>> GetAvailableCars()
        {
            var availableCars = await _dbContext.Cars
                                        .Include(c => c.ContactPerson)
                                        .ThenInclude(c => c.Location)
                                        .Include(c => c.Location)
                                        .Select(c => _mapper.Map<CarDto>(c))
                                        .ToListAsync();
            return availableCars;
        }
    }
}
