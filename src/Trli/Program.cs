using Cocona;
using Trli;

var builder = CoconaApp.CreateBuilder();

var app = builder.Build();

app.AddCommand("board", async ([Ignore] CancellationToken ct) =>
{
	ApiService service = new();
	var results = await service.GetBoardsAsync(ct);
	Console.WriteLine(results.ToConsoleString());
});

app.Run();
