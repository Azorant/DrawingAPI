namespace DrawingAPI.Models;

public class Position
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
}

public class TextPosition : Position
{
    public string? Vertical { get; set; }
    public string? Horizontal { get; set; }
    public string? CalculateX { get; set; }
    public string? CalculateY { get; set; }
}

public class ShapePosition : Position
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int? Rotation { get; set; }
}