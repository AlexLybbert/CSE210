using System;

class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing",
        "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    { }

    public override void Run()
    {
        DisplayStartingMessage();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        bool breatheIn = true;

        while (DateTime.Now < endTime)
        {
            Console.Write(breatheIn ? "Breathe in... " : "Breathe out... ");
            int secondsLeft = (int)(endTime - DateTime.Now).TotalSeconds;
            int breathDuration = Math.Min(4, secondsLeft);
            if (breathDuration > 0)
                ShowCountdown(breathDuration);
            Console.WriteLine();
            breatheIn = !breatheIn;
        }

        DisplayEndingMessage();
        LogActivity();
    }
}
