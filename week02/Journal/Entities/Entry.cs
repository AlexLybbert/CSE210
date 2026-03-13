using System.Security.Cryptography.X509Certificates;

namespace Journal.Entities;

public class Entry
{
    public int _id { get; set; }
    public string _date { get; set; }
    public string _promptText { get; set; }
    public string _entryText { get; set; }

    public override string ToString()
    {
        // keep this format in sync with FromString
        return $"{_date:o}|{_promptText}|{_entryText}";
    }

    public static Entry FromString(string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return null;

        var parts = line.Split('|');
        if (parts.Length != 3)
            return null;

        if (!DateTime.TryParse(parts[0], out var date))
            return null;

        return new Entry
        {
            _date = date.ToShortDateString(),
            _promptText = parts[1],
            _entryText = parts[2]
        };
    }

    public void Display()
    {
        
    }
}