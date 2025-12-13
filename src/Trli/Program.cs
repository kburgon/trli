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
	conf.AddCommand("all", async (
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
});

app.Run();
