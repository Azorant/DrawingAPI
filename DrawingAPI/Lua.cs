using DrawingAPI.Modules;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace DrawingAPI;

public class Lua
{
    public static int Handle(string code)
    {
        using var state = new NLua.Lua();
        state.RegisterFunction("measure", typeof(Lua).GetMethod("Measure"));
        var value = state.DoString($$"""
                         local env = {
                           measure = measure
                         }
                         local function run(untrusted_code)
                           local untrusted_function, message = load(untrusted_code, nil, 't', env)
                           if not untrusted_function then return nil, message end
                           return pcall(untrusted_function)
                         end
                         return run [[{{code}}]]
                         """);
        if (value[0] == null || (bool)value[0] == false) throw new Exception("Nothing was returned from lua");
        return Convert.ToInt32(value[1]);
    }

    public static Size Measure(string text, int size, string font, float wrapWidth = -1)
    {
        var measure = TextMeasurer.MeasureSize(text,
            new TextOptions(TextModule.GetFont(font, size)) { FallbackFontFamilies = new[] { TextModule.EmojiFont() }, WrappingLength = wrapWidth });
        return new Size((int)measure.Width, (int)measure.Height);
    }
}