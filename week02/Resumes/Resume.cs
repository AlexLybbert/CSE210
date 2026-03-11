public class Resume
{
    public string _name { get; set; }
    public List<Job> _jobs { get; set; }

    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");
        for (int i = 0; i < _jobs.Count; i++)
        {
            _jobs[i].Display();
        }
    }
}