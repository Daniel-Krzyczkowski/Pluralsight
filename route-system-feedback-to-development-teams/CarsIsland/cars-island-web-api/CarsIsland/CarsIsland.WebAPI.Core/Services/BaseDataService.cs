using AutoMapper;
using CarsIsland.WebAPI.Data;

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
