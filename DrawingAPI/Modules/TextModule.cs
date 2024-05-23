using DrawingAPI.Models.Components;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class TextModule
{
    public static IImageProcessingContext Handle(this IImageProcessingContext context, TextComponent component)
    {
        var textOptions = new RichTextOptions(SystemFonts.CreateFont(component.Font, component.Size))
        {
            Origin = new Point(component.Position.X, component.Position.Y),
            VerticalAlignment = Parsing.ParseVerticalAlignment(component.Position.Vertical),
            HorizontalAlignment = Parsing.ParseHorizontalAlignment(component.Position.Horizontal),
            WrappingLength = component.WrapWidth,
        };
        var brush = Brushes.Solid(Color.Parse(component.Color));
        context.DrawText(textOptions, component.Content, brush);

        return context;
    }
}