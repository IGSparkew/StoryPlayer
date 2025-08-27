
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
            gameStateManager.DecrementMenuIndex();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            gameStateManager.IncrementMenuIndex();
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            gameStateManager.ExecuteAction();
        }

    }

    private void handleScripting()
    {

    }

    public override void draw()
    {
        this.renderer.Draw();
    }
}