using System;
using System.Collections.Generic;

class ListingActivity : Activity
{
    private static readonly List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private List<string> _promptBag = new List<string>();
    private Random _random = new Random();

    public ListingActivity() : base(
        "Listing",
        "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    { }

    private string GetNextPrompt()
    {
        if (_promptBag.Count == 0)
        {
            _promptBag.AddRange(_prompts);
            for (int i = _promptBag.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (_promptBag[i], _promptBag[j]) = (_promptBag[j], _promptBag[i]);
            }
        }
        string item = _promptBag[_promptBag.Count - 1];
        _promptBag.RemoveAt(_promptBag.Count - 1);
        return item;
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("List as many responses as you can to the following prompt:\n");
        Console.WriteLine($"--- {GetNextPrompt()} ---\n");
        Console.Write("You may begin in: ");
        ShowCountdown(5);
        Console.WriteLine();

        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (input != null && input.Trim().Length > 0)
                count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
        DisplayEndingMessage();
        LogActivity();
    }
}
