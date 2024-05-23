using DrawingAPI.Modules;
using SixLabors.Fonts;
using SixLabors.ImageSharp;

namespace DrawingAPI;

public class Lua
{
    public static int Handle(string code)
    {
        using var state = new NLua.Lua();
        state.DoString("import = function () end");
        state.RegisterFunction("measure", typeof(Lua).GetMethod("Measure"));
        var value = state.DoString(code);
        if (value[0] == null) throw new Exception("Nothing was returned from lua");
        return Convert.ToInt32(value[0]);
    }

    public static Size Measure(string text, int size, string font, float wrapWidth = -1)
    {
        var measure = TextMeasurer.MeasureSize(text,
            new TextOptions(TextModule.GetFont(font, size)) { FallbackFontFamilies = new[] { TextModule.EmojiFont() }, WrappingLength = wrapWidth });
        return new Size((int)measure.Width, (int)measure.Height);
    }
}