// ReSharper disable NotAccessedPositionalProperty.Global

using System.Text.Json.Serialization;

namespace WeatherCli.Records;

public record Coord(double? Lon = null, double? Lat = null);

public record Weather(
    int? Id = null,
    string? Main = null,
    string? Description = null,
    string? Icon = null
);

public record Main(
    double? Temp = null,
    [property: JsonPropertyName("feels_like")] double? FeelsLike = null,
    [property: JsonPropertyName("temp_min")] double? TempMin = null,
    [property: JsonPropertyName("temp_max")] double? TempMax = null,
    int? Pressure = null,
    int? Humidity = null,
    [property: JsonPropertyName("sea_level")] int? SeaLevel = null,
    [property: JsonPropertyName("grnd_level")] int? GrndLevel = null
);

public record Wind(double? Speed = null, int? Deg = null, double? Gust = null);

public record Rain([property: JsonPropertyName("1h")] double? OneHour = null);

public record Clouds(int? All = null);

public record Sys(
    int? Type = null,
    int? Id = null,
    string? Country = null,
    long? Sunrise = null,
    long? Sunset = null
);

public record WeatherData(
    Coord? Coord = null,
    List<Weather>? Weather = null,
    string? Base = null,
    Main? Main = null,
    int? Visibility = null,
    Wind? Wind = null,
    Rain? Rain = null,
    Clouds? Clouds = null,
    long? Dt = null,
    Sys? Sys = null,
    int? Timezone = null,
    int? Id = null,
    string? Name = null,
    int? Cod = null
);
