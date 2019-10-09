using CarsIsland.WebAPI.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarsIsland.WebAPI.Core.Services.Interfaces
{
    public interface ICarsDataService
    {
        Task<IList<CarDto>> GetAvailableCars();
    }
}
