using System.Numerics;
using Raylib_cs;

class RendererUtils
{


    public static void DrawWrappedText(string text, Vector2 position, float maxWidth, Font font, int fontSize)
    {

        var words = text.Split(' ');
        string line = "";
        int lineHeight = fontSize + 4;

        foreach (var word in words)
        {
            string candidate = (line.Length == 0) ? word : line + " " + word;
            int width = Raylib.MeasureText(candidate, fontSize);

            if (width > maxWidth)
            {
                // If the word is too wide, draw the current line and start a new one
                Raylib.DrawTextEx(font, line, position, fontSize, 1.0f, Color.White);
                position.Y += lineHeight;
                line = word;
            }
            else
            {
                line = candidate;
            }
        }

        // Draw the last line
        if (line.Length > 0) Raylib.DrawTextEx(font, line, position, fontSize, 1.0f, Color.White);
    }

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