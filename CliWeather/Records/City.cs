// ReSharper disable NotAccessedPositionalProperty.Global

using System.Text.Json.Serialization;

namespace CliWeather.Records;

public record City(
    string? Name = null,
    [property: JsonPropertyName("local_names")] Dictionary<string, string>? LocalNames = null,
    double? Lat = null,
    double? Lon = null,
    string? Country = null,
    string? State = null
);
