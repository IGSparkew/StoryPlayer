using System.Numerics;
using Raylib_cs;

public class SelectedElement : UIElement
{
    public float ThickOfBound { get; set; }
    public TextUI? textUI {get; set;}

    public SelectedElement(Vector2 margin, float thickOfBound, RenderPosition position, Color color) : base(position, color)
    {
        Margin = margin;
        ThickOfBound = thickOfBound;
    }

    public override void draw()
    {
        if (textUI != null)
        {
            Rectangle rec = RendererUtils.getBoundsOfText(textUI.Value, textUI.Position.Position + textUI.Margin, textUI.Font, textUI.FontSize, Margin, 1.0f,textUI.Origin);
            Raylib.DrawRectangleLinesEx(rec, ThickOfBound, Color);
        }
    }

    public override void update(GameStateManager gmsm)
    {
    }
}