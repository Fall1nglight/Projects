using System.Text.Json.Serialization;

namespace TodosCli;

public class Todo
{
    //fields
    [JsonPropertyName("createdAt")]
    private long _createdAt;

    [JsonPropertyName("name")]
    private string _name;

    [JsonPropertyName("importance")]
    private int _importance;

    [JsonPropertyName("description")]
    private string _description;

    [JsonPropertyName("finished")]
    private bool _finished;

    [JsonPropertyName("deadline")]
    private long _deadline;

    [JsonPropertyName("id")]
    private string _id;

    // constructors
    [JsonConstructor]
    public Todo() { }

    public Todo(
        int id,
        string name,
        string description,
        int importance,
        long deadline,
        long createdAt
    )
    {
        _id = id.ToString();
        _name = name;
        _description = description;
        _importance = importance;
        _deadline = deadline;
        _createdAt = createdAt;
    }

    // methods
    public void DisplayDetails()
    {
        Console.WriteLine("=== TODO DETAILS ===");
        Console.WriteLine($"Id: {_id}");
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Importance: {_importance}");
        Console.WriteLine($"Finished: {_finished}");
    }

    // properties
    public long CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Importance
    {
        get => _importance;
        set => _importance = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? throw new ArgumentNullException(nameof(value));
    }

    public bool Finished
    {
        get => _finished;
        set => _finished = value;
    }

    public long Deadline
    {
        get => _deadline;
        set => _deadline = value;
    }

    public string Id
    {
        get => _id;
        set => _id = value ?? throw new ArgumentNullException(nameof(value));
    }
}
