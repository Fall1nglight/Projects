// ReSharper disable ConvertToAutoProperty

using System.Text;
using System.Text.Json;

namespace TodosCli;

public class TodoManager
{
    // fields
    private readonly ApiService _apiService;
    private List<Todo> _todos;

    // constructors
    public TodoManager()
    {
        _apiService = new ApiService();
        _todos = new List<Todo>();
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
                await UpdateTodo();
                break;
            }

            case 4:
            {
                await DeleteTodo();
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

        Console.Write("Importance: ");
        var importance = int.Parse(Console.ReadLine()!);

        Console.Write("Deadline (yyyy-MM-dd): ");
        var deadline = DateTimeOffset.Parse(Console.ReadLine()!);

        var newTodo = new Todo(
            GetNextId(),
            name,
            description,
            importance,
            deadline.ToUnixTimeSeconds(),
            DateTimeOffset.UtcNow.ToUnixTimeSeconds()
        );

        var result = await _apiService.AddTodo(newTodo);

        if (result == null)
        {
            Console.WriteLine("Failed to add TODO.");
            return;
        }

        _todos.Add(result);
    }

    private int GetNextId()
    {
        if (_todos.Count > 0)
            return int.Parse(_todos.Last().Id) + 1;

        return 1;
    }

    private async Task UpdateTodo()
    {
        var (todoId, fieldId, newVal) = PromptUpdatabeField();
        var newField = GetNewFieldAsJson(fieldId, newVal);

        if (newField == null)
        {
            Console.WriteLine("Invalid field ID.");
            return;
        }

        try
        {
            var result = await _apiService.UpdateTodo(todoId, newField);

            if (result == null)
            {
                Console.WriteLine("Failed to update TODO.");
                return;
            }

            for (var i = 0; i < _todos.Count; i++)
            {
                if (string.CompareOrdinal(_todos[i].Id, result.Id) == 0)
                {
                    _todos[i] = result;
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async Task DeleteTodo()
    {
        Console.Write("ID of the TODO to delete > ");
        var id = int.Parse(Console.ReadLine()!);

        if (_todos.Find(t => int.Parse(t.Id) == id) == null)
        {
            Console.WriteLine("Invalid TODO id.");
            return;
        }

        var result = await _apiService.DeleteTodo(id);

        if (result == null)
        {
            Console.WriteLine("Failed to delete TODO.");
            return;
        }

        _todos.RemoveAll(t => string.Compare(t.Id, result.Id, StringComparison.Ordinal) == 0);
    }

    private (int, int, string) PromptUpdatabeField()
    {
        Console.WriteLine("TODO id > ");
        var todoId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Which field would you like to update?");
        Console.WriteLine("[1] Name");
        Console.WriteLine("[2] Description");
        Console.WriteLine("[3] Importance");
        Console.WriteLine("[4] Finished");
        Console.WriteLine("[5] Deadline");

        Console.Write("> ");
        var fieldId = int.Parse(Console.ReadLine()!);

        Console.Write("Enter new value > ");
        var newVal = Console.ReadLine()!;

        return (todoId, fieldId, newVal);
    }

    private StringContent? GetNewFieldAsJson(int fieldId, string newVal)
    {
        return fieldId switch
        {
            1 => new StringContent(
                JsonSerializer.Serialize(new { name = newVal }),
                Encoding.UTF8,
                "application/json"
            ),

            2 => new StringContent(
                JsonSerializer.Serialize(new { description = newVal }),
                Encoding.UTF8,
                "application/json"
            ),

            3 => new StringContent(
                JsonSerializer.Serialize(new { importance = int.Parse(newVal) }),
                Encoding.UTF8,
                "application/json"
            ),

            4 => new StringContent(
                JsonSerializer.Serialize(new { finished = bool.Parse(newVal) }),
                Encoding.UTF8,
                "application/json"
            ),

            5 => new StringContent(
                JsonSerializer.Serialize(
                    new { deadline = DateTimeOffset.Parse(newVal).ToUnixTimeSeconds() }
                ),
                Encoding.UTF8,
                "application/json"
            ),

            _ => null,
        };
    }

    // propertied
    public List<Todo> Todos => _todos;
    public bool HasTodos => _todos.Count > 0;
}
