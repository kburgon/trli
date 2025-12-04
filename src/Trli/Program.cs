using Cocona;
using Flurl;
using Flurl.Http;
using Trli;
const string ApiKeyEnvName = "TRELLO_API_KEY";
const string TokenEnvName = "TRELLO_TOKEN";
const string TrelloBaseApi = "https://api.trello.com/1/";

await CoconaApp.RunAsync(async ([Argument] string command) =>
{
	if (command != "board")
	{
		Console.WriteLine($"ERROR: Command {command} invalid.");
		return;
	}

    var apiKey = Environment.GetEnvironmentVariable(ApiKeyEnvName);
    var apiToken = Environment.GetEnvironmentVariable(TokenEnvName);

	if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiToken))
	{
		Console.WriteLine($"ERROR: Empty API environment variable(s) found.  Please ensure that the environment variables {ApiKeyEnvName} and {TokenEnvName} are defined in the host environment.");
		return;
	}

	var results = await TrelloBaseApi
		.AppendPathSegment("members/me/boards")
		.SetQueryParams(new Dictionary<string, string>
			{
				{ "key", apiKey },
				{ "token", apiToken },
				{ "filter", "open" }
			})
		.WithHeader("Accept", "application/json")
		.GetJsonAsync<List<Board>>();

	if (results == null)
	{
		Console.WriteLine("ERROR: Failed to get results from API.");
		return;
	}
	
	Console.WriteLine(results.ToConsoleString());
});

