using AutoMapper;
using CarsIsland.WebAPI.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsIsland.WebAPI.Core.Services
{
    public class BaseDataService
    {
        protected CarsIslandDbContext _dbContext;
        protected readonly IMapper _mapper;

        public BaseDataService(CarsIslandDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
    }
}
