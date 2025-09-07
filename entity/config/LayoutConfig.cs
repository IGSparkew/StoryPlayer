using System.Numerics;

public class LayoutConfig
{
    public LayoutConfigType Type { get; set; } = LayoutConfigType.NONE;

    public string Id { get; set; } = String.Empty;
    public string Anchor { get; set; } = String.Empty;

    public bool IsWrapped { get; set; } = false;

    // check if size of tow
    public List<int> Margin { get; set; } = new List<int>();

    public int MaxWidth { get; set; } = 0;

    public Vector2 GetMargin()
    {
        if (Margin.Count > 2)
        {
            throw new Exception("can't create vector the margin has more than Tow Element");
        }

        if (Margin.Count == 1)
        {
            throw new Exception("can't create vector the margin can't had one value");
        }

        if (Margin.Count == 0) return Vector2.Zero;

        return new Vector2(Margin[0], Margin[1]);

    }

}