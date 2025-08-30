public class Action
{

    public string Name { get; set; }
    public string Description { get; set; }
    public string Script { get; set; }

    public bool Show { get; set; }

    public bool Blocked { get; set; }

    public bool IsGlobal { get; set; }

    public Dictionary<string, string> Args { get; set; }

    public Action(string name, string description, string script, bool isGlobal, Dictionary<string, string> args)
    {
        Name = name;
        Description = description;
        Script = script;
        IsGlobal = isGlobal;
        Args = args;
        Show = true;
        Blocked = false;
    }

    public Action() : this("", "", "", false, new Dictionary<string, string>()) { }

}