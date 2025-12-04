using System.Text;
using System.Text.Json.Serialization;

namespace Trli;

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

public static class BoardExtensions
{
	public static string ToConsoleString(this IEnumerable<Board> boards)
	{
		var idCharLength = 25;
		var idOrgCharLength = 25;
		var nameCharLength = 20;
		var descCharLength = 20;
		var starredCharLength = 7;

		StringBuilder builder = new();
		builder.AppendPrintColumn("ID", idCharLength);
		builder.AppendPrintColumn("ORGANIZATION_ID", idOrgCharLength);
		builder.AppendPrintColumn("NAME", nameCharLength);
		builder.AppendPrintColumn("DESCRIPTION", descCharLength);
		builder.AppendPrintColumn("STARRED", starredCharLength);
		builder.AppendLine();

		foreach (var board in boards)
		{
			builder.AppendPrintColumn(board.Id, idCharLength);
			builder.AppendPrintColumn(board.OrganizationId, idOrgCharLength);
			builder.AppendPrintColumn(board.Name, nameCharLength);
			builder.AppendPrintColumn(board.Description, descCharLength);
			builder.AppendPrintColumn(board.Starred, starredCharLength);
			builder.AppendLine();
		}

		return builder.ToString();
	}

	public static void AppendPrintColumn(this StringBuilder builder, object value, int colMaxCharLength)
	{
		var valueText = value?.ToString() ?? string.Empty;
		if (valueText.Length > colMaxCharLength)
		{
			builder.Append(valueText.AsSpan(0, colMaxCharLength));
		}
		else if (valueText.Length < colMaxCharLength)
		{
			builder.Append(valueText);
			var extraChars = colMaxCharLength - valueText.Length;
			for (int i = 0; i < extraChars; i++)
			{
				builder.Append(' ');
			}
		}
		else
		{
			builder.Append(valueText);
		}

		builder.Append(' ');
	}
}
