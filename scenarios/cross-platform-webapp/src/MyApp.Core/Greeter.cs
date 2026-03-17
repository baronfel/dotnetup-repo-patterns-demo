namespace MyApp.Core;

public static class Greeter
{
    public static string Greet(string name) =>
        string.IsNullOrWhiteSpace(name)
            ? "Hello, World!"
            : $"Hello, {name}!";
}
