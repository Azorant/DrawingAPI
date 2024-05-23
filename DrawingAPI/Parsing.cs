using DrawingAPI.Models.Components;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace DrawingAPI;

public static class Parsing
{
    public static VerticalAlignment ParseVerticalAlignment(string? value)
    {
        return value?.ToLower() switch
        {
            "center" => VerticalAlignment.Center,
            "bottom" => VerticalAlignment.Bottom,
            _ => VerticalAlignment.Top
        };
    }

    public static HorizontalAlignment ParseHorizontalAlignment(string? value)
    {
        return value?.ToLower() switch
        {
            "center" => HorizontalAlignment.Center,
            "right" => HorizontalAlignment.Right,
            _ => HorizontalAlignment.Left
        };
    }

    public static Color ParseColor(string color)
    {
        if (color.Contains(','))
        {
            var parts = color.Split(',').Select(p => byte.Parse(p)).ToList();
            return parts.Count == 4
                ? Color.FromRgba(parts[0], parts[1], parts[2], parts[3])
                : Color.FromRgb(parts[0], parts[1], parts[2]);
        }

        return Color.Parse(color);
    }

    public static Shape ParseShape(string shape)
    {
        return shape.ToLower() switch
        {
            "rectangle" => Shape.Rectangle,
            "eclipse" or "circle" => Shape.Eclipse
        };
    }
}