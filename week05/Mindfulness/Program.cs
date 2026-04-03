// Exceeds requirements:
// 1. Saves a log of completed activities to activity_log.txt with timestamps, viewable from the menu.
// 2. Shuffle-bag approach ensures every prompt/question is used at least once before any repeats within a session.

using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App\n");
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Start breathing activity");
            Console.WriteLine("  2. Start reflecting activity");
            Console.WriteLine("  3. Start listing activity");
            Console.WriteLine("  4. View activity log");
            Console.WriteLine("  5. Quit");
            Console.Write("\nSelect a choice from the menu: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    activity = new ListingActivity();
                    break;
                case "4":
                    ShowLog();
                    continue;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    continue;
            }

            activity.Run();
        }
    }

    static void ShowLog()
    {
        Console.Clear();
        Console.WriteLine("--- Activity Log ---\n");
        string logPath = "activity_log.txt";
        if (File.Exists(logPath))
            Console.WriteLine(File.ReadAllText(logPath));
        else
            Console.WriteLine("No activities logged yet.");
        Console.WriteLine("\nPress Enter to return to the menu.");
        Console.ReadLine();
    }
}