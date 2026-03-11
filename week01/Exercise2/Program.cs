using System;

// Stretch Challenge: Added +'s and -'s
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage? ");
        int.TryParse(Console.ReadLine(), out int gradePercent);
        string letter = "";

        if (gradePercent >= 93)
        {
            letter = "A";
        }
        else if (gradePercent >= 90 && gradePercent < 93)
        {
            letter = "A-";
        }
        else if (gradePercent >= 87 && gradePercent < 90)
        {
            letter = "B+";
        }
        else if (gradePercent >= 83 && gradePercent < 87)
        {
            letter = "B";
        }
        else if (gradePercent >= 80 && gradePercent < 83)
        {
            letter = "B-";
        }
        else if (gradePercent >= 77 && gradePercent < 80)
        {
            letter = "C+";
        }
        else if (gradePercent >= 73 && gradePercent < 77)
        {
            letter = "C";
        }
        else if (gradePercent >= 70 && gradePercent < 73)
        {
            letter = "C-";
        }
        else if (gradePercent >= 67 && gradePercent < 70)
        {
            letter = "D+";
        }
        else if (gradePercent >= 63 && gradePercent < 67)
        {
            letter = "D";
        }
        else if (gradePercent >= 60 && gradePercent < 63)
        {
            letter = "D-";
        }
        else
        {
            letter = "F";
        }

        Console.WriteLine($"Your grade is: {letter}");
    }
}