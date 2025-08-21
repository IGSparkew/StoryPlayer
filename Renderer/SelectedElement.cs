using System.Numerics;
using Raylib_cs;

public class SelectedElement
{
    public Vector2 Margin { get; set; }

    public float ThickOfBound { get; set; }

    public Color color { get; set; }

    public SelectedElement(Vector2 margin, float thickOfBound, Color color)
    {
        Margin = margin;
        ThickOfBound = thickOfBound;
        this.color = color;
    }

    public void render(TextUI textUI)
    {
        Rectangle rec = RendererUtils.getBoundsOfText(textUI.Value, textUI.Position.Position, textUI.Font, textUI.FontSize, Margin);
        Raylib.DrawRectangleLinesEx(rec, ThickOfBound, color);
    }
}