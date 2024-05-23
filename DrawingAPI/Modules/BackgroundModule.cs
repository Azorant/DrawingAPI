using DrawingAPI.Models.Components;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class BackgroundModule
{
    public static IImageProcessingContext Handle(this IImageProcessingContext context, BackgroundComponent component)
    {
        context.Fill(Parsing.ParseColor(component.Color));
        context.RoundCorners(component.Rounding);
        return context;
    }
}
