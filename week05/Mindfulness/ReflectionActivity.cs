using System;
using System.Collections.Generic;

class ReflectionActivity : Activity
{
    private static readonly List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private static readonly List<string> _questions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    private List<string> _promptBag = new List<string>();
    private List<string> _questionBag = new List<string>();
    private Random _random = new Random();

    public ReflectionActivity() : base(
        "Reflection",
        "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    { }

    private string GetNext(List<string> source, List<string> bag)
    {
        if (bag.Count == 0)
        {
            bag.AddRange(source);
            for (int i = bag.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                (bag[i], bag[j]) = (bag[j], bag[i]);
            }
        }
        string item = bag[bag.Count - 1];
        bag.RemoveAt(bag.Count - 1);
        return item;
    }

    public override void Run()
    {
        DisplayStartingMessage();

        Console.WriteLine("--- Reflect on the following prompt ---\n");
        Console.WriteLine($"> {GetNext(_prompts, _promptBag)}\n");
        Console.WriteLine("When you have something in mind, press Enter to continue.");
        Console.ReadLine();
        Console.WriteLine("Now ponder on each of these questions as they relate to your experience.");
        Console.Write("Beginning in: ");
        ShowCountdown(5);
        Console.Clear();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        while (DateTime.Now < endTime)
        {
            string question = GetNext(_questions, _questionBag);
            Console.Write($"> {question} ");
            int secondsLeft = (int)(endTime - DateTime.Now).TotalSeconds;
            int pauseDuration = Math.Min(5, secondsLeft);
            if (pauseDuration > 0)
                ShowSpinner(pauseDuration);
            Console.WriteLine();
        }

        DisplayEndingMessage();
        LogActivity();
    }
}
