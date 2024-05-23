namespace DrawingAPI.Models.Components;

public class ImageComponent
{
    public required string Filename { get; set; }
    public required ShapePosition Dimension { get; set; }
    public float Rounding { get; set; }
    public float Opacity { get; set; } = 1;
    public ImageCrop? Crop { get; set; }
}

public class ImageCrop
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool Center { get; set; }
}