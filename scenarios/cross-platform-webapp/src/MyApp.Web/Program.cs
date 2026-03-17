using MyApp.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Greeter.Greet("World"));
app.MapGet("/greet/{name}", (string name) => Greeter.Greet(name));

app.Run();
