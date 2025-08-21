
public class SettingStory
{
    public string Init { get; set; }
    public List<string> Boards { get; set; }
    public List<string> Items { get; set; }

    public String Path { get; set; }

    public SettingStory(string init, List<string> boards, List<string> items, string path)
    {
        Init = init;
        Boards = boards;
        Items = items;
        Path = path;
    }

    public SettingStory() : this("", new List<string>(), new List<string>(), "") {}
}