using System.Text.Json;
using DrawingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DrawingAPI.Controllers;

[ApiController]
[Route("image")]
public class ImageController : ControllerBase
{
    [HttpPost("generate/basic"), Consumes("application/json")]
    public async Task<IActionResult> Generate([FromBody] Instructions json)
    {
        try
        {
            var stream = await Generator.Create(json, []);
            stream.Position = 0;
            return File(stream, "image/png");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return BadRequest(new Dictionary<string, string> { { "error", exception.Message } });
        }
    }
    [HttpPost("generate/files"), Consumes("multipart/form-data")]
    public async Task<IActionResult> GenerateWithFiles([FromForm] string json, IFormFile[] files)
    {
        try
        {
            var data = JsonSerializer.Deserialize<Instructions>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (data == null) throw new Exception("json is null");
            var stream = await Generator.Create(data, files);
            stream.Position = 0;
            return File(stream, "image/png");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            return BadRequest(new Dictionary<string, string> { { "error", exception.Message } });
        }
    }
}