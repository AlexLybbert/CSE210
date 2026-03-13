namespace Journal.Services;

public static class PromptGenerator
{
    private static List<string> _prompts => new List<string>
    {
        "How did I see the hand of the Lord in my life today?",
        "What made me feel true joy?",
        "Did I work toward accomplishing my goals today?",
        "How did I exercise faith in Jesus Christ today?",
        "Who's life did I try to uplift today?"
    };

    public static string GetRandomPrompt()
    {
        int randomNumber = new Random().Next(0, 5);
        return _prompts[randomNumber];
    }
}