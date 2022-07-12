using System.Diagnostics;
using Cocona;
using Cocona.Builder;
using cocona_app.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace cocona_app;

public class Program
{
    public static async Task Main(string[] args)
    {
        var sw = Stopwatch.StartNew();
        var builder = CoconaApp.CreateBuilder(args, options =>
        {
        });
        
        ConfigureServices(builder);

        var app = builder.Build();
        
        AddCommands(app);

        await app.RunAsync();
        sw.Stop();
        
        Console.WriteLine($"Elapsed ms: {sw.ElapsedMilliseconds}");
    }
    
    static void ConfigureServices(CoconaAppBuilder appBuilder)
    {
        appBuilder.Services
            .AddTransient<IHelloHandler, HelloHandler>()
            .AddTransient<IGoodbyeHandler, GoodbyeHandler>();
    }

    static void AddCommands(ICoconaCommandsBuilder app)
    {
        app.AddCommand("hello", (IHelloHandler handler, [Argument] string name, bool goodbye) =>
        {
            handler.Hello(name, goodbye);
        });

        app.AddCommand("hello-async", async (IHelloHandler handler, [Argument] string name) =>
        {
            await handler.HelloAsync(name);
        });

        app.AddCommand("goodbye", ([Argument] string name) =>
        {
            Console.WriteLine($"Goodbye, {name}!");
        });
    }
}
