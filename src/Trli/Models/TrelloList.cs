using System.Text.Json.Serialization;

namespace Trli.Models;

public class TrelloList
{
	public string Id { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public bool Closed { get; set; }

	[JsonPropertyName("idBoard")]
	public string BoardId { get; set; } = string.Empty;
}
