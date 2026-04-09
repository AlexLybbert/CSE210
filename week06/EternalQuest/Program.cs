using System;

class Program
{
    static void Main(string[] args)
    {
        // Creativity beyond requirements:
        // 1) Combo system awards +100 bonus points every 5 successful goal records.
        // 2) Rank titles (Beginner/Seeker/Disciple/Champion/Master) are shown with score.
        GoalManager goalManager = new GoalManager();
        goalManager.Start();
    }
}