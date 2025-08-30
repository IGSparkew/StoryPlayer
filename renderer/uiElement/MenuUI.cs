using System.Numerics;
using Raylib_cs;

class MenuUI : UIElement
{
    public List<TextUI> Options { get; set; }
    public SelectedElement SelectedElement { get; set; }

    public Font Font { get; set; }

    public int FontSize { get; set; }

    public int Selected { get; set; }

    public Vector2 SelectedPading { get; set; }

    public bool IsCenter { get; set; }

    public MenuUI(RenderPosition position, Color color, Font font, int fontSize, List<string> options, Vector2 margin, Vector2 selectedPading, bool isCenter = false, int selected = 0) : base(position, color)
    {
        Selected = selected;
        Font = font;
        FontSize = fontSize;
        Margin = margin;
        SelectedPading = selectedPading;
        IsCenter = isCenter;

        Options = new List<TextUI>();
        init(options);
        SelectedElement = new SelectedElement(SelectedPading, 1, RenderPosition._default(), color);
    }

    private void init(List<string> options)
    {
        foreach (string option in options)
        {
            RenderPosition position = Position.Clone();
            position.AddGap(options.IndexOf(option) * (FontSize + 20), false);
            TextUI text = new TextUI(Font, FontSize, option, false, position, Color.White);
            text.Origin = IsCenter ? RendererUtils.CalculateCenterOfText(text) : Vector2.Zero;
            text.Margin = Margin;
            Options.Add(text);
        }
    }

    public override void draw()
    {
        foreach (TextUI option in Options)
        {
            if (Options.IndexOf(option) == Selected)
            {
                SelectedElement.textUI = option;
                SelectedElement.draw();
            }

            option.draw();
        }
    }

    public override void update(GameStateManager gmsm)
    {
        Selected = gmsm.MenuIndex;
    }
}