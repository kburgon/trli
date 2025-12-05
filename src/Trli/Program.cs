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
	conf.AddCommand("all", async ([Option("board")] string boardId, [Option("list")] string? listId, ApiService service) =>
	{
		var results = await service.GetCards(boardId);
		Console.WriteLine(results.ToConsoleString());
	});
});


app.Run();
