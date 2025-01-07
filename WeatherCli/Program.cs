using System.Net.Http.Json;
using System.Text.Json;
using WeatherCli.Enums;
using WeatherCli.Records;

namespace WeatherCli;

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
        var promptCity = Console.ReadLine()!;

        var cities = await GetCitiesFromApi(promptCity, SharedClient, ApiKey);

        if (cities == null)
        {
            Console.WriteLine("Nem találtam egyetlen ilyen várost sem!");
            return;
        }

        Console.WriteLine("Talált városok");
        for (var i = 0; i < cities.Count; i++)
            Console.WriteLine($"[{i + 1}] {cities[i].Name} - {cities[i].Country}");

        Console.Write("Melyik városra gondoltál? > ");

        var promptCityNum = int.Parse(Console.ReadLine()!);

        if (promptCityNum <= 0 || promptCityNum > cities.Count)
        {
            Console.WriteLine("A listán nem szerepel ilyen város!");
            return;
        }

        promptCityNum--;

        var data = await GetWeatherData(cities[promptCityNum], SharedClient, ApiKey);
        if (data == null)
        {
            Console.WriteLine(
                $"A választott város ({cities[promptCityNum].Name} - {cities[promptCityNum].Country}) adatait nem sikerült lekérdezni, próbáld újra később!"
            );
            return;
        }

        DisplayWeatherData(data);
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

            var requestUri =
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

    private static async Task<WeatherData?> GetWeatherData(
        City city,
        HttpClient httpClient,
        string apiKey
    )
    {
        try
        {
            if (city.Lat == null || city.Lon == null)
                throw new Exception(
                    "A keresett város koordinátái nem elérhetőek, próbáld újra később!"
                );

            var roundedLat = double.Round((double)city.Lat, 2);
            var roundedLon = double.Round((double)city.Lon, 2);

            var requestUri =
                $"/data/2.5/weather?lat={roundedLat}&lon={roundedLon}&appid={apiKey}&lang={Languages.hu}&units={Units.metric}";

            var response = await httpClient.GetFromJsonAsync<WeatherData>(requestUri);
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

    private static void DisplayWeatherData(WeatherData weatherData)
    {
        Console.WriteLine($"=== {weatherData.Name} - {weatherData.Sys?.Country} ===");
        Console.WriteLine(
            $"Hőmérséklet: {weatherData.Main?.Temp}C (érzésre: {weatherData.Main?.FeelsLike}C)"
        );
        Console.WriteLine($"Páratartalom: {weatherData.Main?.Humidity}%");
        Console.WriteLine($"Láthatóság: {weatherData.Visibility}km");
        Console.WriteLine($"Légköri nyomás: {weatherData.Main?.Pressure}hPa");
        Console.WriteLine($"Jelenlegi állapot: {weatherData.Weather?[0].Description}");
    }
}
