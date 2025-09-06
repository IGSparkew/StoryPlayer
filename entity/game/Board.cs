public class Board
{
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;

    public Dictionary<string, string> Connections { get; set; } = new Dictionary<string, string>();

    public List<Action> Actions { get; set; } = new List<Action>();

    public List<Event> Events { get; set; } = new List<Event>();

    // script name to execute on enter board, if empty execute nothing
    public string OnEnter { get; set; } = String.Empty;

    // script name to execute on exit board, if empty execute nothing
    public string OnExit { get; set; } = String.Empty;

    public bool isAction(int index)
    {
        return index > (Connections.Count - 1);
    }
}