namespace CarsIsland.Core.Entities
{
    public class Car : BaseEntity
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ImageUrl { get; set; }
        public decimal Cost { get; set; }
        public string Location { get; set; }
    }
}
