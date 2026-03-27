using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var videos = new List<Video>
        {
            new Video("Top 10 Coffee Hacks", "Daily Brew", 485)
            {
                Comments =
                {
                    new Comment("Shallan", "Hack #3 actually saved my mornings."),
                    new Comment("Jasnah", "Great tips, but I wish there were measurements."),
                    new Comment("The Lopen", "The cold brew trick is genius."),
                    new Comment("Adolin", "Please make a part two!")
                }
            },
            new Video("Budget Desk Setup 2026", "Workspace Weekly", 732)
            {
                Comments =
                {
                    new Comment("Teft", "This gave me a lot of ideas for my tiny apartment."),
                    new Comment("Sigzil", "Could you link the monitor arm?"),
                    new Comment("Kaladin", "Super clean and practical setup."),
                    new Comment("Dalinar", "Loved the cable management section.")
                }
            },
            new Video("Intro to C# Classes", "Code Corner", 615)
            {
                Comments =
                {
                    new Comment("Kelsier", "Finally understood constructors, thank you."),
                    new Comment("Vin", "Can you cover inheritance next?"),
                    new Comment("Ham", "Clear explanation and great pace."),
                    new Comment("Breeze", "The examples were easy to follow.")
                }
            },
            new Video("5K Training Plan for Beginners", "Run Smart", 540)
            {
                Comments =
                {
                    new Comment("Navani", "Week 2 was hard but worth it."),
                    new Comment("Renarin", "Loved the warm-up recommendations."),
                    new Comment("Elend", "This helped me stay consistent."),
                    new Comment("Spook", "Please do a 10K follow-up plan.")
                }
            }
        };

        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length (seconds): {video.LengthInSeconds}");
            Console.WriteLine($"Number of comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.Comments)
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.Text}");
            }

            Console.WriteLine();
        }
    }
}