using System.Text;
using System.Text.Json;
using Font = Microsoft.Maui.Graphics.Font;

namespace presto_maui;
public class ScoreDrawable : IDrawable
{
    private const int FONT_SIZE = 100;
    public static float EM(float value) => FONT_SIZE * value;
    public static GlyphNames GlyphNames { get; private set; }
    public static BravuraMetadata Bravura { get; private set; }
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Font = new Font("Bravura");
        canvas.FontSize = FONT_SIZE;

        //canvas.FontColor = Colors.Gray;
        //canvas.DrawString("\u2014", 0, 0, 100, 100, HorizontalAlignment.Left, VerticalAlignment.Bottom);
        //canvas.DrawString("|", 0, 0, 100, 100, HorizontalAlignment.Left, VerticalAlignment.Bottom);

        canvas.StrokeSize = 3;
        canvas.StrokeColor = Colors.White;
        //DrawGlyph(GlyphNames.staff5Lines.codepoint, EM(0.5f));
        //canvas.DrawLine(0, 0, EM(0.5f), 0);
        //canvas.DrawLine(0, 0, EM(0.5f), 0);
        DrawStaves(EM(10));

        canvas.FontColor = Colors.White;
        //float gclefOffset = EM(Bravura.glyphAdvanceWidths.gClef / 2);
        float gclefOffset = EM(-2.684f / 2 + 2);
        canvas.DrawString(GetGlyph(GlyphNames.gClef.codepoint), 0, gclefOffset, 500, 500, HorizontalAlignment.Left, VerticalAlignment.Top);
        canvas.DrawString(GetGlyph(GlyphNames.noteheadBlack.codepoint), EM(1), EM(-1.25f), 500, 500, HorizontalAlignment.Left, VerticalAlignment.Top);

        void DrawStaves(float length, int lines = 5)
        {
            for (int i = 0; i < lines; i++)
            {
                float y = EM(2) + EM(0.25f) * i;
                canvas.DrawLine(0, y, length, y);
            }
        }

        void DrawGlyph(string codepoint, float x = 0, float y = 0)
        {
            canvas.DrawString(GetGlyph(codepoint), x, y, 500, 500, HorizontalAlignment.Left, VerticalAlignment.Bottom);
        }
    }
    public static string GetGlyph(string codepoint)
    {
        var gclef = codepoint.Replace("U+", "");
        var c = (char)Convert.ToInt16(gclef, 16);
        return c.ToString();
    }
    async public static void DeserializeGlyphNames()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("glyphnames.json");
        GlyphNames = await JsonSerializer.DeserializeAsync<GlyphNames>(stream);
    }
    //async public static void DeserializeMetadata()
    //{
    //    using var stream = await FileSystem.OpenAppPackageFileAsync("bravura_metadata.json");
    //    Bravura = await JsonSerializer.DeserializeAsync<BravuraMetadata>(stream);
    //}
}