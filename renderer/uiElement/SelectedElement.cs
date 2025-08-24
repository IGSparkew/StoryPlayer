using System.Numerics;
using Raylib_cs;

public class SelectedElement : UIElement
{
    public float ThickOfBound { get; set; }
    public TextUI? textUI {get; set;}

    public SelectedElement(Vector2 margin, float thickOfBound, RenderPosition position,  Color color) : base(position , color)
    {
        Margin = margin;
        ThickOfBound = thickOfBound;
    }

    public override void draw()
    {
        if (textUI != null)
        {
            Rectangle rec = RendererUtils.getBoundsOfText(textUI.Value, textUI.Position.Position, textUI.Font, textUI.FontSize, Margin);
            Raylib.DrawRectangleLinesEx(rec, ThickOfBound, Color);
        }
    }
}