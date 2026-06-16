namespace APIService.Services;

public static class StaticResponseFormatter
{
    public static DateTimeOffset GetGeneratedAtUtc()
    {
        return DateTimeOffset.UtcNow;
    }

    public static string FormatMessage(string value)
    {
        return $"{value}: {DateTimeOffset.UtcNow:O}";
    }
}
