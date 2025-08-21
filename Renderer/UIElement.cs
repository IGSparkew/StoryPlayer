using System.Numerics;
using Raylib_cs;

public abstract class UIElement
{
    public RenderPosition Position { get; protected set; }
    public Color Color { get; protected set; }

    public UIElement(RenderPosition position, Color color)
    {
        this.Position = position;
        this.Color = color;
    }
}