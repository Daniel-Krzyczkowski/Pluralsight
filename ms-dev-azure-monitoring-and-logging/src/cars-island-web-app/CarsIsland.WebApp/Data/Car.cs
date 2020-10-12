using System.Text.Json.Serialization;

namespace CarsIsland.WebApp.Data
{
    public class Car
    {
        [JsonPropertyName("brand")]
        public string Brand { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("pricePerDay")]
        public decimal PricePerDay { get; set; }
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
