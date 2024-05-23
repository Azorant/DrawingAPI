using DrawingAPI.Models.Components;

namespace DrawingAPI.Models;

public class Instructions
{
    public int Width { get; set; }
    public int Height { get; set; }
    public BackgroundComponent? Background { get; set; }
    public List<Layer> Layers { get; set; } = new();
}

public class Layer
{
    public TextComponent? Text { get; set; }
    public ShapeComponent? Shape { get; set; }
    public ImageComponent? Image { get; set; }
}