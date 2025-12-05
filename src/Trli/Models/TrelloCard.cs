using System.Text.Json.Serialization;

namespace Trli.Models;

public record TrelloCard
{
	public string Id { get; set; } = string.Empty;

	public bool Closed { get; set; }

	public bool DueComplete { get; set; }

	[JsonPropertyName("desc")]
	public string Description { get; set; } = string.Empty;

	public DateTime? Due { get; set; } = null;

	[JsonPropertyName("idBoard")]
	public string BoardId { get; set; } = string.Empty;

	[JsonPropertyName("idList")]
	public string ListId { get; set; } = string.Empty;

	[JsonPropertyName("idShort")]
	public int ShortId { get; set; }

	public string Name { get; set; } = string.Empty;

	public string ShortUrl { get; set; } = string.Empty;

	public DateTime? Start { get; set; } = null;

	[JsonPropertyName("idChecklists")]
	public List<string> ChecklistIds { get; set; } = [];

	public List<TrelloChecklist>? Checklists { get; set; }
}
