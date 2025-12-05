using System.Text.Json.Serialization;

namespace Trli.Models;

public record TrelloCheckItem
{
	public string Id { get; set; } = string.Empty;

	public string Name { get; set; } = string.Empty;

	public string State { get; set; } = string.Empty;

	public DateTime? Due { get; set; } = null;

	[JsonPropertyName("idChecklist")]
	public string ChecklistId { get; set; } = string.Empty;
}
