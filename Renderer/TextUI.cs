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
}