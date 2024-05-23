using DrawingAPI.Models.Components;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI.Modules;

public static class ImageModule
{
    public static async Task<IImageProcessingContext> Handle(this IImageProcessingContext context, ImageComponent component, IFormFile file)
    {
        var image = (await Image.LoadAsync(file.OpenReadStream())).CloneAs<Rgba32>();
        if (component.Crop != null)
        {
            int xPos = component.Crop.X;
            int yPos = component.Crop.Y;
            int width = component.Crop.Width;
            int height = component.Crop.Height;
            if (component.Crop.Center)
            {
                var ratio = (float)image.Width / image.Height;
                if (Math.Abs(ratio - 1f) > 0)
                {
                    var smallestSize = image.Height > image.Width ? image.Width : image.Height;
                    xPos = image.Width / 2 - smallestSize / 2;
                    yPos = image.Height / 2 - smallestSize / 2;
                    width = smallestSize;
                    height = smallestSize;
                }
            }

            if (width > 0 && height > 0) image.Mutate(x => x.Crop(new Rectangle(xPos, yPos, width, height)));
        }

        image.Mutate(x => x.Resize(new Size(component.Dimension.Width, component.Dimension.Height)).RoundCorners(component.Rounding));

        context.DrawImage(image, new Point(component.Dimension.X, component.Dimension.Y), component.Opacity);

        return context;
    }
}