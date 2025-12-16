using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Trli;
using Trli.Models;

var builder = CoconaApp.CreateBuilder();

builder.Services.AddTransient<ApiService>();

var app = builder.Build();

app.AddCommand("board", async (ApiService service) =>
{
	var results = await service.GetBoardsAsync();
	Console.WriteLine(results.ToConsoleString());
});

app.AddSubCommand("list", conf =>
{
	conf.AddCommand("show", async (
        [Option("board", Description = "The ID of the board to filter lists to.")] string boardId,
        ApiService service) => 
	{
		var results = await service.GetLists(boardId);
		Console.WriteLine(results.ToConsoleString());
	});
});

app.AddSubCommand("card", conf =>
{
	conf.AddCommand("show", async (
        [Option("board", Description = "The ID of the board to filter cards to.")] string boardId,
        [Option("list", Description = "The ID of the list to filter card to.")] string? listId,
		[Option("card", Description = "The ID of the card to display.")] string? cardId,
        ApiService service) =>
	{
		if (cardId != null)
		{
			var cardResult = await service.GetCardAsync(cardId);
			Console.WriteLine(cardResult.ToConsoleString());
			return;
		}

		var results = await service.GetCards(boardId, listId);
		Console.WriteLine(results.ToConsoleString());
	});

	conf.AddCommand("create", async (
        [Option("list", Description = "The ID of the list to add the card to.")] string listId,
        [Option("name", Description = "The name or title of the card to create.")] string cardName,
        [Option("description", Description = "The details contained in the description of the card to create.")] string? description,
		ApiService service) =>
	{
		var results = await service.CreateCardAsync(listId, cardName, description);
		Console.WriteLine(results.ToConsoleString());
	});

	conf.AddCommand("update", async (
        [Argument("id")] string cardId,
        [Option("name", Description = "The value to update the card's name to.")] string? name,
        [Option("description", Description = "The value to update the card's description to.")] string? description,
        [Option("list", Description = "The ID of the list to move the card to.")] string? listId,
        [Option("complete", Description = "Boolean value determining if the card has been completed or not.")] bool complete,
        [Option("notcomplete", Description = "Boolean value determining if the card has been completed or not.")] bool notcomplete,
        ApiService service) =>
	{
		if (complete && notcomplete)
		{
			throw new ArgumentException("Cannot include \"--complete\" and \"--notcomplete\" in the same command.");
		}

		// Logic to translate the two flags determining whether to update the completion status of the card.
		// If none of the options were included, we want to leave the bool null so that it doesn't update.
		// But inclusion of any of the options that should set the completed flag to true/false should update accordingly.
		bool? completed;
		if (complete && !notcomplete)
		{
			completed = true;
		}
		else if (notcomplete && !complete)
		{
			completed = false;
		}
		else
		{
			completed = null;
		}

		var results = await service.UpdateCardAsync(cardId, name, description, listId, completed);
		Console.WriteLine(results.ToConsoleString());
	});
});

app.Run();
