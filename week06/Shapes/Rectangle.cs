public class Rectangle : Shape
{
    private double _length { get; set; }
    private double _width { get; set; }

    public Rectangle(double length, double width, string color) : base(color)
    {
        _length = length;
        _width = width;
    }

    public override double GetArea()
    {
        return _length * _width;
    }
}
