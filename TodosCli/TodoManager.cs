// ReSharper disable ConvertToAutoProperty

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
                UpdateTodo();
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

    private void UpdateTodo() { }

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

        _todos.RemoveAll(t => string.Compare(t.Id, result.Id, StringComparison.Ordinal) == 1);
    }

    // propertied
    public List<Todo> Todos => _todos;
    public bool HasTodos => _todos.Count > 0;
}
