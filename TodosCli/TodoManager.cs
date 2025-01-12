﻿// ReSharper disable ConvertToAutoProperty

namespace TodosCli;

public class TodoManager
{
    // fields
    private readonly ApiService _apiService;
    private readonly DateTimeOffset _dto;
    private List<Todo> _todos;

    // constructors
    public TodoManager()
    {
        _apiService = new ApiService();
        _todos = new List<Todo>();
        _dto = new DateTimeOffset();
    }

    // methods
    public async Task Load()
    {
        try
        {
            _todos = await _apiService.FetchTodos();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public void DisplayTodos()
    {
        if (!HasTodos)
        {
            Console.WriteLine($"No TODOs found.");
            return;
        }

        Console.WriteLine("=== TODOs ===");

        foreach (var todo in _todos)
        {
            Console.WriteLine($"[{todo.Id}] {todo.Name}");
        }
    }

    public async Task PromptOperation()
    {
        Console.WriteLine("=== OPERATIONS ===");
        Console.WriteLine("[1] View TODO");
        Console.WriteLine("[2] Add TODO");
        Console.WriteLine("[3] Update TODO");
        Console.WriteLine("[4] Delete TODO");

        Console.Write("Choose > ");
        var menuNum = int.Parse(Console.ReadLine()!);

        switch (menuNum)
        {
            case 1:
            {
                ViewTodo();
                break;
            }

            case 2:
            {
                await AddTodo();
                break;
            }

            case 3:
            {
                UpdateTodo();
                break;
            }

            case 4:
            {
                DeleteTodo();
                break;
            }

            default:
            {
                Console.WriteLine("Invalid menu number.");
                break;
            }
        }
    }

    private void ViewTodo()
    {
        Console.Write("TODO id > ");
        var todoId = int.Parse(Console.ReadLine()!);
        var todo = _todos.Find(t => int.Parse(t.Id) == todoId);

        if (todo == null)
        {
            Console.WriteLine("Something went wrong!");
            return;
        }

        todo.DisplayDetails();
    }

    private async Task AddTodo()
    {
        Console.WriteLine("=== ADD TODO ===");

        Console.Write("Name: ");
        var name = Console.ReadLine()!;

        Console.Write("Description: ");
        var description = Console.ReadLine()!;

        Console.Write("Importance (1-3): ");
        var importance = int.Parse(Console.ReadLine()!);

        Console.Write("Deadline (yyyy-MM-dd): ");
        var deadline = DateTimeOffset.Parse(Console.ReadLine()!);

        var newTodo = new Todo(
            GetNextId(),
            name,
            description,
            importance,
            deadline.ToUnixTimeSeconds(),
            _dto.ToUnixTimeSeconds()
        );

        var result = await _apiService.AddTodo(newTodo);

        if (result != null)
            _todos.Add(result);
    }

    private int GetNextId()
    {
        return int.Parse(_todos.Last().Id) + 1;
    }

    private void UpdateTodo() { }

    private void DeleteTodo() { }

    // propertied
    public List<Todo> Todos => _todos;
    public bool HasTodos => _todos.Count > 0;
}
