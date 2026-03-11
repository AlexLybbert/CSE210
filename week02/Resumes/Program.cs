using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job()
        {
            _company = "3PL Central",
            _jobTitle = "Software Developer",
            _startYear = 2013,
            _endYear = 2014
        };

        Job job2 = new Job()
        {
            _company = "Avii",
            _jobTitle = "Software Developer",
            _startYear = 2017,
            _endYear = 2022
        };

        Job job3 = new Job()
        {
            _company = "Brandt",
            _jobTitle = "Senior Software Developer",
            _startYear = 2023,
            _endYear = 2026
        };

        Resume resume = new Resume()
        {
            _name = "Alex Lybbert",
            _jobs = new List<Job>
            {
                job1,
                job2,
                job3
            }
        };

        resume.Display();
    }
}