using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class Project
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        [JsonPropertyName("img")]
        public string? BannerImage { get; set; }
        public string? ProjectUrl { get; set; }

        public string? Date { get; set; }
    }
}
