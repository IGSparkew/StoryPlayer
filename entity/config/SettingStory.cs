
public class SettingStory
{
    public string Init { get; set; }
    public List<string> Boards { get; set; }
    public List<string> Items { get; set; }

    public String Path { get; set; }

    public List<LayoutConfig> UIelements { get; set; }

    public SettingStory(string init, List<string> boards, List<string> items, string path, List<LayoutConfig> layoutConfigs)
    {
        Init = init;
        Boards = boards;
        Items = items;
        Path = path;
        UIelements = layoutConfigs;
    }

    public SettingStory() : this("", new List<string>(), new List<string>(), "", new List<LayoutConfig>()) {}
}