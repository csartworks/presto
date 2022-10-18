using Microsoft.Maui.Graphics;
using Font = Microsoft.Maui.Graphics.Font;

namespace presto_maui;
public class ScoreDrawable : IDrawable
{
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Font = new Font("Bravura");
        canvas.FontSize = 36;
        canvas.FontColor = Colors.White;
        canvas.DrawString("\uE050", 100, 100, HorizontalAlignment.Left);
    }
}