using System.Numerics;
using Raylib_cs;

class MenuUI : UIElement
{
    public List<TextUI> Options { get; set; }
    public SelectedElement SelectedElement { get; set; }

    public Font Font { get; set; }

    public int FontSize { get; set; }

    public int Selected { get; set; }

    private List<int> blockedOptions;

    public Vector2 SelectedPading { get; set; }

    public bool IsCenter { get; set; }

    public MenuUI(string Id, RenderPosition position, Color color, Font font, int fontSize, Dictionary<string, string> options, Vector2 margin, Vector2 selectedPading, bool isCenter = false, int selected = 0) : base(Id, position, color)
    {
        Selected = selected;
        Font = font;
        FontSize = fontSize;
        Margin = margin;
        SelectedPading = selectedPading;
        IsCenter = isCenter;
        blockedOptions = new List<int>();
        Options = new List<TextUI>();
        init(options);
        SelectedElement = new SelectedElement("selected", SelectedPading, 1, RenderPosition._default(), color);
    }

    private void init(Dictionary<string, string> options)
    {
        foreach (var option in options)
        {
            RenderPosition position = Position.Clone();
            position.AddGap(options.Keys.ToList().IndexOf(option.Key) * (FontSize + 20), false);
            TextUI text = new TextUI(Font, FontSize, option.Value, false, option.Key, position, Color.White);
            text.Origin = IsCenter ? RendererUtils.CalculateCenterOfText(text) : Vector2.Zero;
            text.Margin = Margin;
            Options.Add(text);
        }
    }

    public override void drawElement()
    {
        List<TextUI> visibleOptions = Options.FindAll(o => o.IsVisible);
        foreach (TextUI option in visibleOptions)
        {
            if (visibleOptions.IndexOf(option) == Selected && !blockedOptions.Contains(visibleOptions.IndexOf(option)))
            {
                SelectedElement.textUI = option;
                SelectedElement.draw();
            }

            option.draw();
        }
    }

    public void blockedOption(string optionId)
    {
        int index = Options.FindIndex(o => o.Id == optionId);
        if (index != -1)
        {
            Options[index].setBlocked();
            blockedOptions.Add(index);
        }
    }

    public void renderOption(string optionId, bool value)
    {
        int index = Options.FindIndex(o => o.Id == optionId);
        if (index != -1)
        {
            Options[index].IsVisible = value;
        }
    }

    public int getLimitSelected()
    {
        return Options.FindAll(o => o.IsVisible).Count - blockedOptions.Count;
    }

    public override void update(GameStateManager gmsm)
    {
        Selected = gmsm.MenuIndex;
    }
}