using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Testing Square ===");
        Square square = new Square(5, "Red");
        Console.WriteLine($"Color: {square._color}");
        Console.WriteLine($"Area: {square.GetArea()}");
        Console.WriteLine();

        Console.WriteLine("=== Testing Rectangle ===");
        Rectangle rectangle = new Rectangle(4, 6, "Blue");
        Console.WriteLine($"Color: {rectangle._color}");
        Console.WriteLine($"Area: {rectangle.GetArea()}");
        Console.WriteLine();

        Console.WriteLine("=== Testing Circle ===");
        Circle circle = new Circle(3, "Green");
        Console.WriteLine($"Color: {circle._color}");
        Console.WriteLine($"Area: {circle.GetArea():F2}");
        Console.WriteLine();

        Console.WriteLine("=== Testing with List<Shape> ===");
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Square(5, "Red"));
        shapes.Add(new Rectangle(4, 6, "Blue"));
        shapes.Add(new Circle(3, "Green"));

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape._color}, Area: {shape.GetArea():F2}");
        }
    }
}