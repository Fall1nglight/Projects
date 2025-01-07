namespace TodosCli;

class Program
{
    public static async Task Main(string[] args)
    {
        TodoManager manager = new TodoManager();
        await manager.Load();
    }
}
