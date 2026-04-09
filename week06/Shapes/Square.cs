using System.Diagnostics.Contracts;

public class Square : Shape
{
    private double _side { get; set; }

    public Square(double side, string color) : base(color)
    {
        _side = side;
    }

    public override double GetArea()
    {
        return _side * _side;
    }
}