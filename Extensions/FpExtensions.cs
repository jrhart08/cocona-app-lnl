namespace cocona_app.Extensions;

public static class FpExtensions
{
    public static T2 Pipe<T1, T2>(this T1 source, Func<T1, T2> next)
        => next(source);

    public static async Task<T2> PipeAsync<T1, T2>(this T1 source, Func<T1, Task<T2>> next)
        => await next(source);

    public static async Task<T2> PipeAsync<T1, T2>(this Task<T1> source, Func<T1, Task<T2>> next)
        => await next(await source);

    public static T Tap<T>(this T value, Action<T> action)
    {
        action(value);

        return value;
    }

    public static async Task<T> TapAsync<T>(this Task<T> value, Action<T> action)
    {
        var val = await value;

        action(val);

        return val;
    }

    public static async Task<T> TapAsync<T>(this Task<T> value, Func<T, Task> task)
    {
        var val = await value;

        await task(val);

        return val;
    }

    public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> left, Func<T2, T3> right)
        => t1 => right(left(t1));
}