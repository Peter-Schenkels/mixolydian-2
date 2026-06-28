using System.Text.Json;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class Art : ITemInterface
    {
        public string? Name { get; set; }
        [JsonPropertyName("img")]
        public string? Image { get; set; }
        public override string ToString() => JsonSerializer.Serialize<Art>(this);
        public string? Date { get; set; }
    }
}
