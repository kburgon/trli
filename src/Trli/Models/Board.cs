using System.Text;
using System.Text.Json.Serialization;

namespace Trli.Models;

public record Board
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = string.Empty;

	[JsonPropertyName("idOrganization")]
	public string OrganizationId { get; set; } = string.Empty;

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("desc")]
	public string Description { get; set; } = string.Empty;

	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;

	[JsonPropertyName("starred")]
	public bool Starred { get; set; } = false;

	[JsonPropertyName("shortUrl")]
	public string ShortUrl { get; set; } = string.Empty;
}

