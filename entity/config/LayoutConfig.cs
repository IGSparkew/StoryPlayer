using System.Numerics;

public class LayoutConfig
{
    public string Type { get; set; }

    public string Id { get; set; }
    public string Anchor { get; set; }

    public bool IsWrapped { get; set; }

    // check if size of tow
    public List<int> Margin { get; set; }

    public int MaxWidth { get; set; }

    public LayoutConfig()
    {
        Type = "";
        Anchor = "";
        Id = "";
        IsWrapped = false;
        Margin = new List<int>();
        MaxWidth = 0;
    }

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