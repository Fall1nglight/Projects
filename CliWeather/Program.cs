using System.Net.Http.Json;
using System.Text.Json;
using CliWeather.Records;

namespace CliWeather;

class Program
{
    private static readonly HttpClient SharedClient = new HttpClient()
    {
        BaseAddress = new Uri("http://api.openweathermap.org"),
    };

    private const string ApiKey = "";

    public static async Task Main(string[] args)
    {
        Console.Write("Add meg a város nevét: ");
        string promptCity = Console.ReadLine()!;

        var cities = await GetCitiesFromApi(promptCity, SharedClient, ApiKey);

        if (cities == null)
        {
            Console.WriteLine("Nem találtam egyetlen ilyen várost sem!");
            return;
        }

        Console.WriteLine("Talált városok");
        foreach (var city in cities)
        {
            Console.WriteLine($"Város: {city.name}");
            Console.WriteLine($"Ország: {city.country}");
        }
    }

    private static async Task<List<City>?> GetCitiesFromApi(
        string city,
        HttpClient httpClient,
        string apiKey
    )
    {
        try
        {
            if (string.IsNullOrEmpty(city))
                throw new ArgumentException("Üres városra nem kereshetsz.", nameof(city));

            string requestUri =
                $"/geo/1.0/direct?q={Uri.EscapeDataString(city)}&limit=5&appid={apiKey}";

            var response = await httpClient.GetFromJsonAsync<List<City>>(requestUri);
            return response;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HTTP hiba történt: {e.Message}");
        }
        catch (JsonException e)
        {
            Console.WriteLine($"JSON hiba történt: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ismeretlen hiba történt: {e.Message}");
        }

        return null;
    }
}
