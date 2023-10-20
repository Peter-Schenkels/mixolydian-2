using System.Text.Json;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class Music
    {
        public string? Name { get; set; }
        public string? Genres { get; set; }

        [JsonPropertyName("cover")]
        public string? Cover { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }
        [JsonPropertyName("artist")]
        public string? Artis { get; set; }
        [JsonPropertyName("date")]
        public string? Date { get; set;}


        public override string ToString() => JsonSerializer.Serialize<Music>(this);
    }
}
