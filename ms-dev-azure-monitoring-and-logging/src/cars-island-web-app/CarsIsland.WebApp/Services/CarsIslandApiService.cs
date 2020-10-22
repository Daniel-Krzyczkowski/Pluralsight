using CarsIsland.WebApp.Data;
using CarsIsland.WebApp.Extensions;
using CarsIsland.WebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarsIsland.WebApp.Services
{
    public class CarsIslandApiService : ICarsIslandApiService
    {
        private readonly HttpClient _httpClient;

        public CarsIslandApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Car>> GetAvailableCarsAsync()
        {
            var response = await _httpClient.GetAsync("api/car/all");
            return await response.ReadContentAs<List<Car>>();
        }

        public async Task SendEnquiryAsync(string attachmentFileName, ContactFormModel enquiry)
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(enquiry.Title), "title");
            multipartContent.Add(new StringContent(enquiry.Content), "content");
            multipartContent.Add(new StringContent(enquiry.CustomerContactEmail), "customerContactEmail");
            if (enquiry.Attachment != null)
            {
                multipartContent.Add(new StreamContent(enquiry.Attachment), "Attachment", attachmentFileName);
            }
            await _httpClient.PostAsync("api/enquiry", multipartContent);
        }
    }
}
