
using System.Text.Json;

interface IStoryParser
{
    Dictionary<string, SettingStory> GetSettingsStories();
    GameStateManager constructStoryContext(string storyName);
}


class StoryParser : IStoryParser
{
    private Dictionary<string, SettingStory> settingsStories;

    public StoryParser()
    {
        settingsStories = new Dictionary<string, SettingStory>();
        LoadSettingsStories();
        ServiceLoader.RegisterService<IStoryParser>("StoryParser", this);
    }

    public Dictionary<string, SettingStory> GetSettingsStories()
    {
        return settingsStories;
    }

    public GameStateManager constructStoryContext(string storyName)
    {
        if (!settingsStories.ContainsKey(storyName))
        {
            throw new KeyNotFoundException($"Story with name '{storyName}' not found.");
        }

        GameStateManager gameStateManager = new GameStateManager();
        SettingStory settingStory = settingsStories[storyName];
        gameStateManager.SettingStory = settingStory;
        List<Item> items = loadItemsFromStory(settingStory);
        List<Board> boards = loadBoardsFromStory(settingStory);

        foreach (var board in boards)
        {
            gameStateManager.BoardManager.AddBoard(board.Name, board);
        }

        foreach (var item in items)
        {
            gameStateManager.AddItemInWorld(item);
        }

        gameStateManager.SetCurrentBoard(settingStory.Init);

        return gameStateManager;
    }

    private List<Board> loadBoardsFromStory(SettingStory settingStory)
    {
        List<Board> boards = new List<Board>();

        string boardPathFolder = Path.Combine(settingStory.Path, "boards");

        string[] boardFiles = Directory.GetFiles(boardPathFolder, "*.json");
        foreach (var boardFile in boardFiles)
        {
            var json = File.ReadAllText(boardFile);
            Board? board = JsonSerializer.Deserialize<Board>(json);
            if (board != null)
            {
                boards.Add(board);
            }
        }


        return boards;
    }

    private List<Item> loadItemsFromStory(SettingStory settingStory)
    {
        List<Item> items = new List<Item>();
        string itemPathFolder = Path.Combine(settingStory.Path, "items");

        string[] itemFiles = Directory.GetFiles(itemPathFolder, "*.json");
        foreach (var itemFile in itemFiles)
        {
            var json = File.ReadAllText(itemFile);
            Item? item = JsonSerializer.Deserialize<Item>(json);
            if (item != null)
            {
                items.Add(item);
            }
        }
        

        return items;
    }

    private void LoadSettingsStories()
    {
        if (!Directory.Exists(Settings.STORY_PATH))
        {
            throw new DirectoryNotFoundException($"Story path '{Settings.STORY_PATH}' does not exist.");
        }

        foreach (var dir in Directory.GetDirectories(Settings.STORY_PATH))
        {
            string path = Path.Combine(dir, "settings.json");
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                SettingStory? settingStory = JsonSerializer.Deserialize<SettingStory>(json);
                if (settingStory != null)
                {
                    settingStory.Path = dir;
                    string name = Path.GetFileName(dir.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
                    settingsStories.Add(name, settingStory);
                }
            }
        }
    }
    
    



}