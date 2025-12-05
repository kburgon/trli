using System.Text.Json.Serialization;

namespace Trli.Models;

public record TrelloChecklist
{
	public string Id { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("idBoard")]
	public string BoardId { get; set; } = string.Empty;

	[JsonPropertyName("idCard")]
	public string CardId { get; set; } = string.Empty;

	[JsonIgnore]
	public List<TrelloCheckItem> CheckItems { get; set; } = [];
}
