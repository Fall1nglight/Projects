namespace FolderExplorer;

class Program
{
    public static void Main(string[] args)
    {
        string path = @"C:\Windows\Globalization";
        DisplayDirectory(path);
        Console.ReadLine();
    }

    private static void DisplayDirectory(string path, int depth = 0)
    {
        if (!Directory.Exists(path))
        {
            Console.WriteLine("The given directory does not exist!");
            return;
        }

        Console.WriteLine($"{new string('\t', depth)}{path}");

        // files
        string[] files = GetFiles(path);
        foreach (string file in files)
            Console.WriteLine($"{new string('\t', depth + 1)}{file}");

        // folders
        string[] folders = GetFolders(path);
        foreach (string folder in folders)
            DisplayDirectory(folder, depth + 1);
    }

    private static string[] GetFolders(string path)
    {
        if (!Directory.Exists(path))
            return Array.Empty<string>();

        return Directory.GetDirectories(path);
    }

    private static string[] GetFiles(string path)
    {
        if (!Directory.Exists(path))
            return Array.Empty<string>();

        return Directory.GetFiles(path);
    }
}
