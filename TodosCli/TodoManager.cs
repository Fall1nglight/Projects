// ReSharper disable ConvertToAutoProperty
using TodosCli.Records;

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

    // propertied
    public List<Todo> Todos => _todos;
}
