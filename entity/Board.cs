public class Board
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Dictionary<string, string> Connections { get; set; }

    public List<Action> Actions { get; set; }

    // script name to execute on enter board, if empty execute nothing
    public string OnEnter { get; set; }

    // script name to execute on exit board, if empty execute nothing
    public string OnExit { get; set; }


    public Board(string name, string description) : this(name, description, new Dictionary<string, string>(), new List<Action>(), "", "")
    {
    }

    public Board(string name, string description, Dictionary<string, string> connections, List<Action> actions, String onEnter, String onExit)
    {
        Name = name;
        Description = description;
        Connections = connections;
        Actions = actions;
        OnEnter = onEnter;
        OnExit = onExit;
    }

    public Board() : this("", "")
    {

    }

    public int getLimitSelected()
    {
        int limit = Connections.Count - 1;

        foreach (var action in Actions)
        {
            if (action.IsGuided)
            {
                limit += 1;
            }
        }

        return limit;
    }

    public bool isAction(int index)
    {
        return index > (Connections.Count - 1);
    }
}