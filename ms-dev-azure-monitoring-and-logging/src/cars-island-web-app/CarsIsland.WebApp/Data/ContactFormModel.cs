using System.IO;
using System.Text.Json.Serialization;

namespace CarsIsland.WebApp.Data
{
    public class ContactFormModel
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("customerContactEmail")]
        public string CustomerContactEmail { get; set; }

        public string AttachmentFileName { get; set; }
        public Stream Attachment { get; set; }
    }
}
