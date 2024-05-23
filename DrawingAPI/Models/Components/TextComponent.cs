namespace DrawingAPI.Models.Components;

public class TextComponent
{
    public required string Content { get; set; }
    public int Size { get; set; } = 12;
    public string Font { get; set; } = "Noto Sans";
    public string Color { get; set; } = "ffffff";
    public float WrapWidth { get; set; } = -1f;
    public TextPosition Position { get; set; } = new();
}