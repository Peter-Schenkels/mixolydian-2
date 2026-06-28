using System.Text.Json;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class Music : ITemInterface
    {
        public string? Name { get; set; }
        public string? Genres { get; set; }
        public string? Type { get; set; }

        [JsonPropertyName("cover")]
        public string? Cover { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("artist")]
        public string? Artis { get; set; }


        public override string ToString() => JsonSerializer.Serialize<Music>(this);
        public string? Date { get; set; }
    }
}
