using DrawingAPI.Models;
using DrawingAPI.Modules;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace DrawingAPI;

public static class Generator
{
    public static async Task<MemoryStream> Create(Instructions instructions, IFormFile[] files)
    {
        Image image = new Image<Rgba32>(instructions.Width, instructions.Height);
        if (instructions.Background != null)
        {
            image.Mutate(x => x.Handle(instructions.Background));
        }

        foreach (var layer in instructions.Layers)
        {
            Image imageLayer = new Image<Rgba32>(instructions.Width, instructions.Height);


            if (layer.Shape != null)
            {
                imageLayer.Mutate(x => x.Handle(layer.Shape));
            }

            if (layer.Text != null)
            {
                imageLayer.Mutate(x => x.Handle(layer.Text));
            }

            if (layer.Image != null)
            {
                imageLayer.Mutate(x =>
                {
                    var file = files.FirstOrDefault(f => f.FileName == layer.Image.Filename);
                    if (file == null) throw new Exception($"Unable to find upload named {layer.Image.Filename}");
                    var task = Task.Run(async () => { await x.Handle(layer.Image, file); });
                    task.Wait();
                });
            }

            image.Mutate(x => x.DrawImage(imageLayer, 1f));
        }

        var stream = new MemoryStream();
        await image.SaveAsync(stream, PngFormat.Instance);
        return stream;
    }
}