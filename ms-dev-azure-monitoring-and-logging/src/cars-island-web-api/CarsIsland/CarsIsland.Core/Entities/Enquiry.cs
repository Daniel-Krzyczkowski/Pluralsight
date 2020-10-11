namespace CarsIsland.Core.Entities
{
    public class Enquiry : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string CustomerContactEmail { get; set; }
    }
}
