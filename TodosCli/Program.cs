namespace TodosCli;

class Program
{
    public static async Task Main(string[] args)
    {
        TodoManager manager = new TodoManager();
        await manager.Load();
        manager.DisplayTodos();
        await manager.PromptOperation();
    }
}
