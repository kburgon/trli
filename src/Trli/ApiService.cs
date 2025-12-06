using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl;
using Flurl.Http;
using Trli.Models;

namespace Trli;

public class ApiService
{
	const string ApiKeyEnvName = "TRELLO_API_KEY";
	const string TokenEnvName = "TRELLO_TOKEN";
	const string TrelloBaseApi = "https://api.trello.com/1/";
    private const string EmptyEnvVariableErrorMessage = $"ERROR: Empty API environment variable(s) found.  Please ensure that the environment variables {ApiKeyEnvName} and {TokenEnvName} are defined in the host environment.";

    private readonly string? _trelloApiKey = Environment.GetEnvironmentVariable(ApiKeyEnvName);
	private readonly string? _trelloToken = Environment.GetEnvironmentVariable(TokenEnvName);
	
	public async Task<List<Board>> GetBoardsAsync(CancellationToken? cancellationToken = null)
	{
		if (string.IsNullOrEmpty(_trelloApiKey) || string.IsNullOrEmpty(_trelloToken))
		{
			throw new NullReferenceException(EmptyEnvVariableErrorMessage);
		}

		var results = await TrelloBaseApi
			.AppendPathSegment("members/me/boards")
			.SetQueryParams(new Dictionary<string, string>
			{
				{ "key", _trelloApiKey },
				{ "token", _trelloToken },
				{ "filter", "open" }
			})
			.WithHeader("Accept", "application/json")
			.GetJsonAsync<List<Board>>();

		return results;
	}

	public async Task<List<TrelloList>> GetLists(string boardId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(_trelloApiKey) || string.IsNullOrEmpty(_trelloToken))
		{
			throw new NullReferenceException(EmptyEnvVariableErrorMessage);
		}

		if (string.IsNullOrEmpty(boardId))
		{
			throw new ArgumentException($"{nameof(boardId)} cannot be empty.");
		}

		var results = await TrelloBaseApi
			.AppendPathSegment($"boards/{boardId}/lists")
			.SetQueryParams(new Dictionary<string, string>
			{
				{ "key", _trelloApiKey },
				{ "token", _trelloToken }
			})
			.WithHeader("Accept", "application/json")
			.GetJsonAsync<List<TrelloList>>(cancellationToken: cancellationToken);

		return results;
	}

	public async Task<List<TrelloCard>> GetCards(string boardId, string? listId = null, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(_trelloApiKey) || string.IsNullOrEmpty(_trelloToken))
		{
			throw new NullReferenceException(EmptyEnvVariableErrorMessage);
		}

		if (string.IsNullOrEmpty(boardId))
		{
			throw new ArgumentException($"{nameof(boardId)} cannot be empty.");
		}

		var results = await TrelloBaseApi
			.AppendPathSegment($"boards/{boardId}/cards")
			.SetQueryParams(new Dictionary<string, string>
			{
				{ "key", _trelloApiKey },
				{ "token", _trelloToken },
				{ "checklists", "all"}
			})
			.WithHeader("Accept", "application/json")
			.GetJsonAsync<List<TrelloCard>>(cancellationToken: cancellationToken);

		if (!string.IsNullOrEmpty(listId))
		{
			return [.. results.Where(card => card.ListId == listId)];
		}

		return results;
	}

	public async Task<TrelloCard> GetCardAsync(string cardId, CancellationToken cancellationToken = default)
	{
		if (string.IsNullOrEmpty(cardId))
		{
			throw new ArgumentException($"{nameof(cardId)} cannot be empty.");
		}

		if (string.IsNullOrEmpty(_trelloApiKey) || string.IsNullOrEmpty(_trelloToken))
		{
			throw new NullReferenceException(EmptyEnvVariableErrorMessage);
		}

		var results = await TrelloBaseApi
			.AppendPathSegment($"/cards/{cardId}")
			.SetQueryParams(new Dictionary<string, object>
			{
				{ "key", _trelloApiKey },
				{ "token", _trelloToken },
				{ "checklists", "all"},
				{ "checkItemStates", "true" }
			})
			.WithHeader("Accept", "application/json")
			.GetJsonAsync<TrelloCard>(cancellationToken: cancellationToken);
		
		return results;
	}
}
