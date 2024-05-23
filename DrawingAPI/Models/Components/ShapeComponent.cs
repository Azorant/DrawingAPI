namespace DrawingAPI.Models.Components;

public class ShapeComponent
{
    public required string Shape { get; set; }
    public required ShapePosition Dimension { get; set; }
    public string Color { get; set; } = "000000";
    public float Rounding { get; set; }
}

public enum Shape
{
    Rectangle,
    Eclipse
}
