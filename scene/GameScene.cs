
using Raylib_cs;

class GameScene : Scene
{
    public override void init(Dictionary<string, object> parameters)
    {
        string storyName = "";

        if (parameters.ContainsKey(SceneManager.SELECTED_STORY))
        {
            storyName = parameters[SceneManager.SELECTED_STORY].ToString() ?? "";
        }

        if (string.IsNullOrEmpty(storyName))
        {
           throw new Exception("Error no story selected");
        }

        this.gameStateManager = storyParser.constructStoryContext(storyName);
        this.renderer.setCurrentRendererBoard(this.gameStateManager);
    }

    public override void update(float dt)
    {
        renderer.Update(gameStateManager);

        if (gameStateManager.IsInputMenu)
        {
            handleScripting();
        }
        else
        {
            handleInput();
        }
    }

    private void handleInput()
    {
        //TODO change to setup mult key config

        gameStateManager.update();

        if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            gameStateManager.MenuIndex = (gameStateManager.MenuIndex - 1 + GetMenuUI().getLimitSelected()) % GetMenuUI().getLimitSelected();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            gameStateManager.MenuIndex = (gameStateManager.MenuIndex + 1) % GetMenuUI().getLimitSelected();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            gameStateManager.ExecuteAction();
        }

    }

     private MenuUI GetMenuUI()
    {
        SettingStory? settingStory = gameStateManager.SettingStory;
        if (settingStory == null)
        {
            throw new Exception("Error no setting story found");
        }

        string elementId = "";

        settingStory.UIelements.ForEach(config =>
        {
            if (config.Type == LayoutConfigType.MenuUI)
            {
                elementId = config.Id;
            }
        });

        if (elementId == "")
        {
            throw new Exception("Error no menu ui found");
        }

        MenuUI menu = (MenuUI)this.renderer.GetUiElement(elementId);
        return menu;
    }

    private void handleScripting()
    {

    }

    public override void draw()
    {
        this.renderer.Draw();
    }
}