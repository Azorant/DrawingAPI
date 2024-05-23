using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class UtilModule
{
    public static IImageProcessingContext RoundCorners(this IImageProcessingContext context, float cornerRadius)
    {
        if (cornerRadius == 0) return context;
        Size size = context.GetCurrentSize();
        IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);

        context.SetGraphicsOptions(new GraphicsOptions()
        {
            Antialias = true,

            AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
        });

        return corners.Aggregate(context, (current, path) => current.Fill(Color.Yellow, path));
    }

    private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
    {
        var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);

        IPath cornerTopLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

        float rightPos = imageWidth - cornerTopLeft.Bounds.Width + 1;
        float bottomPos = imageHeight - cornerTopLeft.Bounds.Height + 1;

        IPath cornerTopRight = cornerTopLeft.RotateDegree(90).Translate(rightPos, 0);
        IPath cornerBottomLeft = cornerTopLeft.RotateDegree(-90).Translate(0, bottomPos);
        IPath cornerBottomRight = cornerTopLeft.RotateDegree(180).Translate(rightPos, bottomPos);

        return new PathCollection(cornerTopLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
    }

    public static IPath ApplyRoundCorners(RectangularPolygon rectangularPolygon, float radius)
    {
        if (radius == 0) return rectangularPolygon;
        var squareSize = new SizeF(radius, radius);
        var ellipseSize = new SizeF(radius * 2, radius * 2);
        var offsets = new[]
        {
            (0, 0),
            (1, 0),
            (0, 1),
            (1, 1)
        };
        var holes = offsets.Select(
            offset =>
            {
                var squarePos = new PointF(
                    offset.Item1 == 0 ? rectangularPolygon.Left : rectangularPolygon.Right - radius,
                    offset.Item2 == 0 ? rectangularPolygon.Top : rectangularPolygon.Bottom - radius
                );
                var circlePos = new PointF(
                    offset.Item1 == 0 ? rectangularPolygon.Left + radius : rectangularPolygon.Right - radius,
                    offset.Item2 == 0 ? rectangularPolygon.Top + radius : rectangularPolygon.Bottom - radius
                );
                return new RectangularPolygon(squarePos, squareSize)
                    .Clip(new EllipsePolygon(circlePos, ellipseSize));
            }
        );
        return rectangularPolygon.Clip(holes);
    }
}