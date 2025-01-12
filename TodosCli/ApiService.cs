﻿using System.Net.Http.Json;

namespace TodosCli;

public class ApiService
{
    // fields
    private readonly HttpClient _httpClient;

    // constructors
    public ApiService()
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://677cf53b4496848554c85b2f.mockapi.io/api/v1/todos"),
        };
    }

    // methods
    public async Task<List<Todo>> FetchTodos()
    {
        var todos = new List<Todo>();

        try
        {
            todos = await _httpClient.GetFromJsonAsync<List<Todo>>(string.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return todos ?? new List<Todo>();
    }

    public async Task<Todo?> AddTodo(Todo newTodo)
    {
        try
        {
            using var response = await _httpClient.PostAsJsonAsync("/todos", newTodo);
            response.EnsureSuccessStatusCode();

            var todo = await response.Content.ReadFromJsonAsync<Todo>();
            return todo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }

    // properties
}
