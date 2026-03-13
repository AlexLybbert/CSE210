namespace Journal.Entities;

public static class Journal
{
    public static List<Entry> _entries { get; set; }

    public static void AddEntry(Entry entry)
    {
        Database.AddEntity(entry);
    }

    public static void DisplayAllEntries()
    {
        _entries = Database.GetAll<Entry>();

        foreach (var entry in _entries)
        {
            Console.WriteLine();
            Console.WriteLine(entry);
        }
    }

    public static void RemoveMostRecentEntry()
    {
        Database.DeleteRecent<Entry>();
    }

    public static void SaveToFile(string filePath)
    {
        // Ensure the directory exists before writing the file
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var entries = Database.GetAll<Entry>();
        using (var writer = new StreamWriter(filePath))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
    }

    public static void LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var entry = Entry.FromString(line);
            if (entry != null)
                Database.AddEntity(entry);
        }
    }
}