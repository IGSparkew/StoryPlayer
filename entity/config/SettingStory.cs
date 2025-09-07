
public class SettingStory
{
    public string Title { get; set; } = String.Empty;

    public string Init { get; set; } = String.Empty;
    public List<string> Boards { get; set; } = new List<string>();
    public List<string> Items { get; set; } = new List<string>();

    public String Path { get; set; } = String.Empty;

    public List<LayoutConfig> UIelements { get; set; } = new List<LayoutConfig>();

}