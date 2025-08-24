public class LayoutConfig
{

    string Type { get; set; }
    string Anchor { get; set; }

    // check if size of tow
    List<int> Margin { get; set; }

    public LayoutConfig()
    {
        Type = "";
        Anchor = "";
        Margin = new List<int>();
    }

}