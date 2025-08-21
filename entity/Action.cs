public class Action
{

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsGuided { get; set; }
    public bool Active { get; set; }
    public string Script { get; set; }
    public bool IsGlobal { get; set; }
    public Dictionary<string, string> Args { get; set; }

    public Action(string name, string description, bool isGuided, string script, Dictionary<string, string> args, bool isGlobal = false)
    {
        Name = name;
        Description = description;
        IsGuided = isGuided;
        Script = script;
        Active = true;
        Args = args;
        IsGlobal = isGlobal;
    }

    public Action() : this("", "", false, "", new Dictionary<string, string>(), true) { }

}