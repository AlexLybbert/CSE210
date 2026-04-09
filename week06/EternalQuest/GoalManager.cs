using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _comboCount;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _comboCount = 0;
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");

            int choice = PromptForInt("Select a choice from the menu: ", 1, 6);
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    ListGoalDetails();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    running = false;
                    break;
            }

            if (running)
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        string rank = GetRank();
        Console.WriteLine($"You have {_score} points. Rank: {rank}");
    }

    public void ListGoalNames()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals found.");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void ListGoalDetails()
    {
        ListGoalNames();
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");

        int goalType = PromptForInt("Which type of goal would you like to create? ", 1, 3);
        string shortName = PromptForString("What is the name of your goal? ");
        string description = PromptForString("What is a short description of it? ");
        int points = PromptForInt("What is the amount of points associated with this goal? ", 1);

        switch (goalType)
        {
            case 1:
                _goals.Add(new SimpleGoal(shortName, description, points));
                break;
            case 2:
                _goals.Add(new EternalGoal(shortName, description, points));
                break;
            case 3:
                int target = PromptForInt("How many times does this goal need to be accomplished for a bonus? ", 1);
                int bonus = PromptForInt("What is the bonus for accomplishing it that many times? ", 1);
                _goals.Add(new ChecklistGoal(shortName, description, points, target, bonus));
                break;
        }

        Console.WriteLine("Goal created.");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available. Create a goal first.");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].GetDetailsString()}");
        }

        int goalNumber = PromptForInt("Which goal did you accomplish? ", 1, _goals.Count);
        Goal selectedGoal = _goals[goalNumber - 1];
        int pointsEarned = selectedGoal.RecordEvent();

        if (pointsEarned > 0)
        {
            _score += pointsEarned;
            _comboCount++;
            Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");

            // Creativity: every 5 successful events gives a combo bonus.
            if (_comboCount % 5 == 0)
            {
                const int comboBonus = 100;
                _score += comboBonus;
                Console.WriteLine($"Combo bonus! +{comboBonus} points for {_comboCount} completed events.");
            }
        }
        else
        {
            Console.WriteLine("That goal is already complete, so no additional points were awarded.");
        }

        Console.WriteLine($"Your current score is: {_score}");
    }

    public void SaveGoals()
    {
        string filename = PromptForString("What is the filename for the goal file? ");
        List<string> lines = new List<string>
        {
            $"META|{_score}|{_comboCount}"
        };

        foreach (Goal goal in _goals)
        {
            lines.Add(goal.GetStringRepresentation());
        }

        File.WriteAllLines(filename, lines);
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals()
    {
        string filename = PromptForString("What is the filename for the goal file? ");

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _score = 0;
        _comboCount = 0;

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] parts = line.Split('|');

            if (parts[0] == "META" && parts.Length >= 3)
            {
                int.TryParse(parts[1], out _score);
                int.TryParse(parts[2], out _comboCount);
                continue;
            }

            Goal goal = ParseGoal(parts);
            if (goal != null)
            {
                _goals.Add(goal);
            }
        }

        Console.WriteLine("Goals loaded.");
    }

    private Goal ParseGoal(string[] parts)
    {
        if (parts.Length == 0)
        {
            return null;
        }

        switch (parts[0])
        {
            case "SimpleGoal":
                if (parts.Length >= 5 && int.TryParse(parts[3], out int simplePoints) && bool.TryParse(parts[4], out bool isComplete))
                {
                    return new SimpleGoal(parts[1], parts[2], simplePoints, isComplete);
                }
                break;

            case "EternalGoal":
                if (parts.Length >= 4 && int.TryParse(parts[3], out int eternalPoints))
                {
                    return new EternalGoal(parts[1], parts[2], eternalPoints);
                }
                break;

            case "ChecklistGoal":
                if (parts.Length >= 7
                    && int.TryParse(parts[3], out int checklistPoints)
                    && int.TryParse(parts[4], out int target)
                    && int.TryParse(parts[5], out int bonus)
                    && int.TryParse(parts[6], out int completed))
                {
                    return new ChecklistGoal(parts[1], parts[2], checklistPoints, target, bonus, completed);
                }
                break;
        }

        return null;
    }

    private int PromptForInt(string prompt, int minValue)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= minValue)
            {
                return value;
            }

            Console.WriteLine($"Please enter a number greater than or equal to {minValue}.");
        }
    }

    private int PromptForInt(string prompt, int minValue, int maxValue)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= minValue && value <= maxValue)
            {
                return value;
            }

            Console.WriteLine($"Please enter a number between {minValue} and {maxValue}.");
        }
    }

    private string PromptForString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("Input cannot be empty.");
        }
    }

    private string GetRank()
    {
        if (_score >= 5000)
        {
            return "Master";
        }

        if (_score >= 3000)
        {
            return "Champion";
        }

        if (_score >= 1500)
        {
            return "Disciple";
        }

        if (_score >= 500)
        {
            return "Seeker";
        }

        return "Beginner";
    }
}
