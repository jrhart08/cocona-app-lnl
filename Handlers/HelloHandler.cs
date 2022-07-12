using cocona_app.Extensions;

namespace cocona_app.Handlers;

public interface IHelloHandler
{
    void Hello(string name, bool goodbye);
    Task HelloAsync(string name);
}

public class HelloHandler : IHelloHandler
{
    readonly IGoodbyeHandler _goodbye;

    public HelloHandler(IGoodbyeHandler goodbye)
    {
        _goodbye = goodbye;
    }

    public void Hello(string name, bool goodbye)
    {
        object oName = name;

        object sName2 = oName switch { string { Length: >= 5 } s => s[..5], var o => o };
        
        Console.WriteLine($"sName2: {sName2}");

        string? sName = oName switch
        {
            string s when s.StartsWith("J") => "J-Dawg",
            string { Length: >= 5 } s => s[..5],
            string s => s,
            var o => o.ToString(),
        };

        string formatted = sName!
            .Pipe(s => s.ToUpper())
            .Tap(Console.WriteLine)
            .Pipe(s => s.Trim())
            .Tap(Console.WriteLine);

        Task<string> formattedAsync = Task.FromResult(sName!)
            .PipeAsync(s => Task.FromResult(s.ToUpper()));

        Console.WriteLine($"Hello, {formatted}!");
        if (goodbye)
        {
            _goodbye.AndGoodbye();
        }
    }

    public async Task HelloAsync(string name)
    {
        int length = await name
            .PipeAsync(ToUpperAsync)
            .TapAsync(Console.WriteLine)
            .PipeAsync(ReverseAsync)
            .TapAsync(Console.WriteLine)
            .PipeAsync(LengthAsync)
            .TapAsync(Console.WriteLine)
            .TapAsync(async _ => await Task.Delay(1000))
            .TapAsync(async s => Console.WriteLine(s));
        
        Console.WriteLine($"Hello {name}, asynchronously!");
    }

    static async Task<string> ToUpperAsync(string str)
    {
        await Task.Delay(1000);

        return str.ToUpper();
    }

    static async Task<string> ReverseAsync(string str)
    {
        await Task.Delay(1000);

        return string.Join("", str.Reverse());
    }

    static async Task<int> LengthAsync(string str)
    {
        await Task.Delay(1000);

        return str.Length;
    }
}