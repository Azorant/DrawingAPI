using DrawingAPI.Models.Components;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class TextModule
{
    public static FontFamily EmojiFont()
    {
        FontCollection fonts = new();
        fonts.Add("./Resources/Twemoji.Mozilla.ttf");
        return fonts.Get("Twemoji Mozilla");
    }

    public static Font GetFont(string font, int size)
    {
        try
        {
            return SystemFonts.CreateFont(font, size);
        }
        catch (Exception)
        {
            FontCollection fonts = new();
            return fonts.Add("./Resources/Roboto-Regular.ttf").CreateFont(size);
        }
    }

    public static IImageProcessingContext Handle(this IImageProcessingContext context, TextComponent component)
    {
        var x = string.IsNullOrEmpty(component.Position.CalculateX) ? component.Position.X : Lua.Handle(component.Position.CalculateX);
        var y = string.IsNullOrEmpty(component.Position.CalculateY) ? component.Position.Y : Lua.Handle(component.Position.CalculateY);
        var textOptions = new RichTextOptions(GetFont(component.Font, component.Size))
        {
            FallbackFontFamilies = new[]
            {
                EmojiFont()
            },
            Origin = new Point(x, y),
            VerticalAlignment = Parsing.ParseVerticalAlignment(component.Position.Vertical),
            HorizontalAlignment = Parsing.ParseHorizontalAlignment(component.Position.Horizontal),
            WrappingLength = component.WrapWidth,
        };
        var brush = Brushes.Solid(Color.Parse(component.Color));
        context.DrawText(textOptions, component.Content, brush);

        return context;
    }
}