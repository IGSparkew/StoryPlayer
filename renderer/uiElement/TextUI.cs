using System.Numerics;
using Raylib_cs;

public class TextUI : UIElement
{
    public string Value { get; set; }
    public bool IsWrapped { get; set; }

    public bool RenderBounds { get; set; }

    public int MaxWidth { get; set; }

    public Font Font { get; set; }

    public int FontSize { get; set; }

    public TextUI(Font font, int fontSize, string value, bool isWrapped, RenderPosition position, Color color, int maxWidth = 0, bool renderBounds = false) : base(position, color)
    {
        Font = font;
        FontSize = fontSize;
        Value = value;
        IsWrapped = isWrapped;
        MaxWidth = maxWidth;
        RenderBounds = renderBounds;
    }

    public override void draw()
    {
        if (IsWrapped)
        {
            drawWrapped();
        }
        else
        {
            drawText(Position.Position, Value);
        }
    }

    private void drawWrapped()
    {

        var words = Value.Split(' ');
        string line = "";
        int lineHeight = FontSize + 4;

        Vector2 wrappedPos = Position.Position;

        foreach (var word in words)
        {
            string candidate = (line.Length == 0) ? word : line + " " + word;
            int width = Raylib.MeasureText(candidate, FontSize);

            if (width > MaxWidth)
            {
                // If the word is too wide, draw the current line and start a new one
                drawText(wrappedPos, line);
                wrappedPos.Y += lineHeight;
                line = word;
            }
            else
            {
                line = candidate;
            }
        }

        // Draw the last line
        if (line.Length > 0) drawText(wrappedPos, line);
    }

    private void drawText(Vector2 position, string line)
    {
        Raylib.DrawTextEx(Font, line, position + Margin, FontSize, 1.0f, Color);
    }

    public override void update(GameStateManager gmsm)
    {
    }
}