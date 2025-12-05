using System.Text;
using Cocona.Command.BuiltIn;

namespace Trli.Models;

public static class Extensions
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

	public static string ToConsoleString(this List<TrelloList> lists)
	{
		StringBuilder builder = new();
		var idMaxLength = 25;
		var nameMaxLength = 25;
		var closedMaxLength = 6;
		var boardIdMaxLength = 25;

		builder.AppendPrintColumn("ID", idMaxLength);
		builder.AppendPrintColumn("BOARD_ID", boardIdMaxLength);
		builder.AppendPrintColumn("NAME", nameMaxLength);
		builder.AppendPrintColumn("CLOSED", closedMaxLength);
		builder.AppendLine();

		foreach (var list in lists)
		{
			builder.AppendPrintColumn(list.Id, idMaxLength);
			builder.AppendPrintColumn(list.BoardId, boardIdMaxLength);
			builder.AppendPrintColumn(list.Name, nameMaxLength);
			builder.AppendPrintColumn(list.Closed, closedMaxLength);
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
