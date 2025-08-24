using System.Numerics;
using Raylib_cs;

class RendererUtils
{
    public static Rectangle getBoundsOfText(string text, Vector2 position, Font font, int fontSize, Vector2 margin, float spacing = 1.0F)
    {
         Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, spacing);
        return new Rectangle(
            position.X - margin.X,
            position.Y - margin.Y,
            textSize.X + (margin.X * 2),
            textSize.Y + (margin.Y * 2)
        );
    }

}