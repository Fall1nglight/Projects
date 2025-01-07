# Weather Information Application

This is a C# console application that allows users to search for cities and retrieve weather information based on their selection. The application interacts with the OpenWeatherMap API to fetch city data and weather details.

## Project Overview

1. The user enters a city name, and the application queries the OpenWeatherMap API to find matching cities.
2. The user is presented with a list of cities, and they can select the one they are referring to.
3. After selecting a city, the application fetches the weather data for that city using another API endpoint.
4. The weather information, including temperature, humidity, visibility, and atmospheric pressure, is displayed to the user.

## Features
- **City Search**: The user can search for cities by name and choose from a list of matching cities.
- **Weather Data Retrieval**: Once a city is selected, the weather data such as temperature, humidity, and weather conditions are displayed.
- **Error Handling**: The application handles potential errors from the API, including network issues or missing data.

## API Usage

The application interacts with the **OpenWeatherMap API**. This is a popular weather service that provides weather data for cities worldwide.

### Endpoints Used:
1. **/geo/1.0/direct**: Fetches a list of cities that match the entered search query.
    - Endpoint example: `/geo/1.0/direct?q={cityName}&limit=5&appid={apiKey}`
2. **/data/2.5/weather**: Fetches weather details for a specific city using its latitude and longitude.
    - Endpoint example: `/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&lang={language}&units={units}`

### API Key:
- You will need to sign up for an OpenWeatherMap account to get your API key.
- Replace the `ApiKey` constant in the code with your API key.

## Requirements
- A C# compatible environment to run the application (e.g., Visual Studio, Visual Studio Code, or Rider).
- An active internet connection to fetch data from the OpenWeatherMap API.

## Installation
1. Clone the repository to your local machine.
2. Open the project in your preferred C# IDE.
3. Replace the `ApiKey` constant in the `Program.cs` file with your OpenWeatherMap API key.
4. Run the application in the terminal/console.
5. Follow the on-screen prompts to search for cities and view weather data.