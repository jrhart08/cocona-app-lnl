namespace cocona_app.Handlers;

public interface IGoodbyeHandler
{
    void Goodbye(string name);
    void AndGoodbye();
}

public class GoodbyeHandler : IGoodbyeHandler
{
    public void Goodbye(string name)
    {
        Console.WriteLine($"Goodbye, {name}!");
    }

    public void AndGoodbye()
    {
        Console.WriteLine("And goodbye!");
    }
}