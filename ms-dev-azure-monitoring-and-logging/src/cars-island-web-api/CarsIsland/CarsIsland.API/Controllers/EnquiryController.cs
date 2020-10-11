using CarsIsland.Core.Entities;
using CarsIsland.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarsIsland.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly ILogger<CarController> _logger;
        private readonly IDataRepository<Enquiry> _enquiryRepository;

        public EnquiryController(ILogger<CarController> logger,
                             IDataRepository<Enquiry> enquiryRepository)
        {
            _logger = logger;
            _enquiryRepository = enquiryRepository;


        }
    }
}
