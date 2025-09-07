using Raylib_cs;
class Engine
{
    public Renderer Renderer { get; private set; }
    public RessourceManager RessourceManager { get; private set; }
    public SceneManager SceneManager { get; private set; }

    public ScriptReader ScriptReader { get; private set; }

    public StoryParser StoryParser { get; private set; }

    public Engine()
    {
        init();
        RessourceManager = new RessourceManager();
        loadDefaultResources();
        ScriptReader = new ScriptReader();
        Renderer = new Renderer();
        StoryParser = new StoryParser();
        SceneManager = new SceneManager();
        SceneManager.changeScene("Menu");
        loop();
        exit();
    }

    private void init()
    {
        Raylib.InitWindow(Settings.WIDTH, Settings.HEIGHT, Settings.GAME_TITLE);
    }

    private void loadDefaultResources()
    {
        RessourceManager.LoadFont(RessourceManager.DEFAULT_FONT_NAME, Settings.ASSET_PATH + "BoldPixels1.4.ttf");
        RessourceManager.LoadTexture("default", Settings.ASSET_PATH + "ti-99-4a.gif");
    }

    private void loop()
    {
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            update(deltaTime);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib_cs.Color.Black);
            draw();
            Raylib.EndDrawing();
        }
    }



    public void update(float deltaTime)
    {
        // Custom update logic goes here
        SceneManager.update(deltaTime);
    }

    public void draw()
    {
        // Custom drawing code goes here
        SceneManager.draw();
    }

    public void exit()
    {
        RessourceManager.UnloadAllResources();
        Raylib.CloseWindow();
    }
 


}