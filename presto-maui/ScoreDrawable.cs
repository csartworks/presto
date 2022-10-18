using System.Text;
using System.Text.Json;
using Font = Microsoft.Maui.Graphics.Font;

namespace presto_maui;
public class ScoreDrawable : IDrawable
{
    public static GlyphNames GlyphNames { get; private set; }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Font = new Font("Bravura");
        canvas.FontSize = 100;
        //DrawGlyph(GlyphNames.gClef.codepoint);
        //DrawGlyph(GlyphNames.staff5Lines.codepoint);
        //canvas.DrawString("O", 100, 100, HorizontalAlignment.Center);
        var centerX = dirtyRect.Center.X;
        var centerY = dirtyRect.Center.Y;

        canvas.FontColor = Colors.Gray;
        canvas.DrawString("\u2014", 0, 0, 100, 100, HorizontalAlignment.Left, VerticalAlignment.Bottom);

        canvas.FontColor = Colors.WhiteSmoke;
        canvas.DrawString("|", 0, 0, 100, 100, HorizontalAlignment.Left, VerticalAlignment.Bottom);

        canvas.Rotate(-90, 50, 50);
        //DrawGlyph(GlyphNames.staff5Lines.codepoint, 0, 0);
        //DrawGlyph(GlyphNames.staff5Lines.codepoint, 100, 100);

        void DrawGlyph(string codepoint, int x = 0, int y = 0)
        {
            canvas.DrawString(GetGlyph(codepoint), x, y, HorizontalAlignment.Center);
        }
    }
    public string GetGlyph(string codepoint)
    {
        var gclef = codepoint.Replace("U+", "");
        var c = (char)Convert.ToInt16(gclef, 16);
        return c.ToString();
    }
    async private Task<string> LoadRaw(string file)
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(file);
        using var reader = new StreamReader(stream);
        var metadata = reader.ReadToEnd();
        return metadata;
    }
    async public static void DeserializeGlyphNames()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("glyphnames.json");
        GlyphNames = await JsonSerializer.DeserializeAsync<GlyphNames>(stream);
    }
}