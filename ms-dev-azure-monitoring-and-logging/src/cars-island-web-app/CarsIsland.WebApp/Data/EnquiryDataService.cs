using CarsIsland.WebApp.Services.Interfaces;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Data
{
    public class EnquiryDataService
    {
        private readonly ICarsIslandApiService _carsIslandApiService;

        public EnquiryDataService(ICarsIslandApiService carsIslandApiService)
        {
            _carsIslandApiService = carsIslandApiService;
        }

        public async Task SendEnquiryAsync(string attachmentFileName, ContactFormModel newEnquiry)
        {
            await _carsIslandApiService.SendEnquiryAsync(attachmentFileName, newEnquiry);
        }
    }
}
