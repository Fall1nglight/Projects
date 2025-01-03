namespace CliWeather.Records;

public record City(
    string? name = null,
    Dictionary<string, string>? local_names = null,
    double? lat = null,
    double? lon = null,
    string? country = null,
    string? state = null
);
