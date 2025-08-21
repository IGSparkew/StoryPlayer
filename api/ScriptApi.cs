public class ScriptApi
{
    private GameStateManager gameStateManager;

    public ScriptApi(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    public void setFlag(string flagName, bool value)
    {
        gameStateManager.SetFlag(flagName, value);
    }

    public bool hasFlag(string flagName)
    {
        return gameStateManager.HasFlag(flagName);
    }

    public bool getFlag(string flagName)
    {
        return gameStateManager.GetFlag(flagName);
    }

    public void addItem(String itemName)
    {
        Item? item = gameStateManager.GetItemFromWorld(itemName);
        if (item == null)
        {
            return;
        }

        gameStateManager.AddItemInInventory(item);
    }

}