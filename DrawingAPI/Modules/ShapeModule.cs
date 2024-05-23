using DrawingAPI.Models.Components;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class ShapeModule
{
    public static IImageProcessingContext Handle(this IImageProcessingContext context, ShapeComponent component)
    {
        var color = Parsing.ParseColor(component.Color);
        switch (Parsing.ParseShape(component.Shape))
        {
            case Shape.Rectangle:
            {
                context.Fill(Brushes.Solid(color),
                    UtilModule.ApplyRoundCorners(new RectangularPolygon(component.Dimension.X, component.Dimension.Y, component.Dimension.Width,
                        component.Dimension.Height), component.Rounding));
                break;
            }
            case Shape.Eclipse:
            {
                context.Fill(Brushes.Solid(color), new EllipsePolygon(component.Dimension.X, component.Dimension.Y, component.Dimension.Width,
                    component.Dimension.Height));
                break;
            }
        }


        return context;
    }
}