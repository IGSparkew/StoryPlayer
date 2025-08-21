public class BoardManager
{
    private Dictionary<string, Board> boards;

    public BoardManager()
    {
        boards = new Dictionary<string, Board>();
    }

    public void AddBoard(string name, Board board)
    {
        boards.Add(name, board);
    }

    public Board GetBoard(string name)
    {
        if (!boards.ContainsKey(name))
        {
            throw new KeyNotFoundException($"Board with name '{name}' not found.");
        }
        return boards[name];
    }

    public void RemoveBoard(string name)
    {
        if (!boards.ContainsKey(name))
        {
            throw new KeyNotFoundException($"Board with name '{name}' not found.");
        }
        boards.Remove(name);
    }

    public string ExecuteAction(Action action, GameStateManager gameStateManager, IScriptReader scriptReader)
    {
        return scriptReader.run(action.Script, gameStateManager, action.IsGlobal, action.Args);
    }
}