
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
	public interface ITemInterface
	{
		[JsonPropertyName("date")]
		public string? Date { get; set;}

	}
}