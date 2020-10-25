using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace CarsIsland.API.Dto
{
    public class CustomerEnquiry
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("customerContactEmail")]
        public string CustomerContactEmail { get; set; }

        public IFormFile Attachment { get; set; }
    }
}
