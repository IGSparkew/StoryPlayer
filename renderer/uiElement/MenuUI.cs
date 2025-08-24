using System.Numerics;
using Raylib_cs;

class MenuUI : UIElement
{
    public List<TextUI> Options { get; set; }
    public SelectedElement SelectedElement { get; set; }

    public Font Font { get; set; }

    public int FontSize { get; set; }

    public int Selected { get; set; }

    public MenuUI(RenderPosition position, Color color, Font font, int fontSize, List<string> options, int selected = 0) : base(position, color)
    {
        Selected = selected;
        Font = font;
        FontSize = fontSize;

        Options = new List<TextUI>();
        init(options);
        SelectedElement = new SelectedElement(new Vector2(10, 10), 1, RenderPosition._default(), color);
    }

    private void init(List<string> options)
    {
        foreach (string option in options)
        {
            RenderPosition position = Position.Clone();
            position.AddGap(options.IndexOf(option) * (FontSize + 20), false);
            TextUI text = new TextUI(Font, FontSize, option, false, position, Color.White);
            Options.Add(text);
        }
    }

    public override void draw()
    {
        foreach (TextUI option in Options)
        {
            option.Position.Position += Margin;

            if (Options.IndexOf(option) == Selected)
            {
                SelectedElement.textUI = option;
                SelectedElement.draw();
            }
            
            option.draw();
        }
    }
}