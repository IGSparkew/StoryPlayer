using System.Numerics;
using Raylib_cs;

public abstract class UIElement
{
    public RenderPosition Position { get; protected set; }
    public Color Color { get; protected set; }

    public Vector2 Margin { get; set; }

    public string Id { get; set; }

    public bool IsVisible { get; set; }

    public UIElement(string Id, RenderPosition position, Color color)
    {
        this.Position = position;
        this.Color = color;
        this.Margin = Vector2.Zero;
        this.Id = Id;
        this.IsVisible = true;
    }

    public abstract void update(GameStateManager gmsm);

    public void draw()
    {
        if (IsVisible)
        {
            drawElement();
        }
    }

    public abstract void drawElement();
}