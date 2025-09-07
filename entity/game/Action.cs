public class Action
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Script { get; set; } = String.Empty;

    public bool Show { get; set; } = true;

    public bool Blocked { get; set; } = false;

    public bool IsGlobal { get; set; } = false;

    public Dictionary<string, string> Args { get; set; } = new Dictionary<string, string>();

}