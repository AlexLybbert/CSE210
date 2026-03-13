// The extra challenge I did was incorporating a database

using Journal.Entities;
using Journal.Services;
using JournalEntity = Journal.Entities.Journal;

class Program
{
    static void Main(string[] args)
    {
        Database.DatabaseInit();

        Console.WriteLine("Welcome to your journal!");
        DisplayOptions();

        string choice = Console.ReadLine();
        bool exit = false;

        while (!exit)
        {
            string fileName;
            switch (choice)
            {
                case "1":
                    string prompt = PromptGenerator.GetRandomPrompt();

                    Console.WriteLine("Here's a prompt to help you get started:");
                    Console.WriteLine(prompt);
                    Console.WriteLine();
                    Console.WriteLine("Writing a new entry...");
                    Console.WriteLine();

                    string entryText = Console.ReadLine();

                    Entry newEntry = new Entry
                    {
                        _entryText = entryText,
                        _date = DateTime.Now.ToShortDateString(),
                        _promptText = prompt
                    };

                    JournalEntity.AddEntry(newEntry);

                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Journal entry saved");
                    Console.ResetColor();
                    break;
                case "2":
                    Console.WriteLine("Displaying the journal...");

                    JournalEntity.DisplayAllEntries();

                    Console.WriteLine();
                    break;
                case "3":
                    Console.WriteLine("What is the path to the file?");

                    fileName = Console.ReadLine();

                    Console.WriteLine("Saving the journal to a file...");

                    JournalEntity.SaveToFile(fileName);

                    Console.WriteLine($"Journal saved to {fileName}");
                    break;
                case "4":
                    Console.WriteLine("What is the path to the file?");

                    fileName = Console.ReadLine();

                    Console.WriteLine("Loading the journal from a file...");

                    JournalEntity.LoadFromFile(fileName);

                    break;
                case "5":
                    Console.WriteLine("Deleting the most recent journal entry...");

                    JournalEntity.RemoveMostRecentEntry();

                    Console.WriteLine("Journal entry deleted.");
                    break;
                case "6":
                    Console.WriteLine("Closing and locking your journal. Goodbye!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (!exit)
            {
                DisplayOptions();
                choice = Console.ReadLine();
            }
        }
    }

    public static void DisplayOptions()
    {
        Console.WriteLine();
        Console.WriteLine("Please choose an option:");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Delete the most recent entry");
        Console.WriteLine("6. Close and lock your journal (so your siblings can't read it!)");
    }
}