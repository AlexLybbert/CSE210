using System.Collections.Generic;

public class Video
{
    public string Title { get; }
    public string Author { get; }
    public int LengthInSeconds { get; }
    public List<Comment> Comments { get; } = new List<Comment>();

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}