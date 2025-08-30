using System.Numerics;
using Raylib_cs;

public abstract class UIElement
{
    public RenderPosition Position { get; protected set; }
    public Color Color { get; protected set; }

    public Vector2 Margin { get; set; }

    public UIElement(RenderPosition position, Color color)
    {
        this.Position = position;
        this.Color = color;
        this.Margin = Vector2.Zero;
    }

    public abstract void update(GameStateManager gmsm);

    public abstract void draw();
}