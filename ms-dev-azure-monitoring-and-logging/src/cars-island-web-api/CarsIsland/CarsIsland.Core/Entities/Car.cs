using System.Text.Json.Serialization;

namespace CarsIsland.Core.Entities
{
    public class Car : BaseEntity
    {
        [JsonPropertyName("brand")]
        public string Brand { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public decimal PricePerDay { get; set; }
        public string Location { get; set; }
    }
}
