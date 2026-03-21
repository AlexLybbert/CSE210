//The extra credit work I did was add an API service that calls to different sites to get the random scripture

using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Exceeding requirement: pull a random scripture from Wikiquote each run.
        WikiBibleService wikiBibleService = new();
        Scripture scripture = await wikiBibleService.GetRandomScriptureAsync();

        if (scripture == null)
        {
            Console.WriteLine("Couldn't find a scripture. Check your internet connection.");
            return;
        }

        while (!scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to continue or type 'quit' to finish: ");

            string input = Console.ReadLine() ?? "";
            if (input.Trim().ToLower() == "quit")
            {
                return;
            }

            scripture.HideRandomWords(3);
        }

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine("All words are now hidden. Great work!");
    }
}
