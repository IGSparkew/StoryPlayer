
public class GameStateManager
{
    private Board? currentBoard;
    private List<Item> inventory;

    private Dictionary<string, Item> worldItems;

    private Dictionary<string, bool> flags;

    public int MenuIndex { get; set; }

    public bool IsInputMenu { get; set; }

    public BoardManager BoardManager { get; set; }

    private IScriptReader scriptReader;

    public SettingStory? SettingStory { get; set; }

    public List<string> logs { get; set; } = new List<string>();

    private bool isUpdatedBoard;

    private EventManager eventManager;

    public bool IsUpdatedBoard
    {
        get
        {
            bool _v = isUpdatedBoard;
            if (_v) isUpdatedBoard = false;

            return _v;
        }
    }

    public GameStateManager()
    {
        inventory = new List<Item>();
        BoardManager = new BoardManager();
        currentBoard = null;
        flags = new Dictionary<string, bool>();
        worldItems = new Dictionary<string, Item>();
        scriptReader = ServiceLoader.GetService<IScriptReader>("ScriptReader");
        this.eventManager = new EventManager(this);
        MenuIndex = 0;
        IsInputMenu = false;
        isUpdatedBoard = false;
    }

    public void SetFlag(string flagName, bool value)
    {
        if (flags.ContainsKey(flagName))
        {
            flags[flagName] = value;
        }
        else
        {
            flags.Add(flagName, value);
        }
    }

    public bool GetFlag(string flagName)
    {
        if (!flags.ContainsKey(flagName))
        {
            throw new KeyNotFoundException($"Flag '{flagName}' not found.");
        }

        return flags[flagName];
    }

    public bool HasFlag(string flagName)
    {
        return flags.ContainsKey(flagName);
    }

    public Board? GetCurrentBoard()
    {
        return currentBoard;
    }

    public void SetCurrentBoard(string boardName)
    {
        Board board = BoardManager.GetBoard(boardName);
        if (board != null)
        {
            if (currentBoard != null && currentBoard.OnExit != "")
            {
                logs.Add("On Exit: " + scriptReader.run(currentBoard.OnExit, this, false));
            }

            currentBoard = board;

            isUpdatedBoard = true;

            if (currentBoard.OnEnter != "")
            {
                logs.Add("On Enter: " + scriptReader.run(currentBoard.OnEnter, this, false));
            }


        }
    }

    public void update()
    {

        if (currentBoard == null) return;

        foreach (string flag in flags.Keys.ToList())
        {
            if (flag.StartsWith("#") && flags[flag])
            {
                foreach (Event evt in currentBoard.Events)
                {
                    if (string.Equals(evt.Flag, flag))
                    {
                        // Execute the event
                        this.eventManager.TriggerEvent(evt);
                    }
                }
            }
        }


        if (logs.Count > 0)
        {
            Console.WriteLine(logs[0]);
            logs.RemoveAt(0);
        }
    }

    public void AddItemInWorld(Item item)
    {
        worldItems.Add(item.Name, item);
    }

    public void RemoveItemFromWorld(string itemName)
    {
        worldItems.Remove(itemName);
    }

    public bool HasItemInWorld(string itemName)
    {
        return worldItems.ContainsKey(itemName);
    }

    public Item? GetItemFromWorld(string itemName)
    {
       return worldItems.ContainsKey(itemName) ? worldItems[itemName] : null;
    }

    public void AddItemInInventory(Item item)
    {
        inventory.Add(item);
    }

    public void RemoveItemFromInventory(Item item)
    {
        inventory.Remove(item);
    }

    public bool HasItemInInventory(Item item)
    {
        return inventory.Contains(item);
    }

    public void ExecuteAction()
    {
        if (currentBoard == null) return;

        if (currentBoard.isAction(MenuIndex))
        {
            // TODO change how to move from board
            int index = MenuIndex - currentBoard.Connections.Count;
            Action action = currentBoard.Actions[index];
            String result = this.BoardManager.ExecuteAction(action, this, scriptReader);
            logs.Add(result);
            // Todo add something to do with result
        }
        else
        {
            string key = currentBoard.Connections.Keys.ElementAt(MenuIndex);
            this.SetCurrentBoard(key);
        }
    }
}