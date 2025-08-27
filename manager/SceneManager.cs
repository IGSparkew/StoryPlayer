interface ISceneManager
{
    void changeScene(string sceneName, Dictionary<string, object>? parameters = null);
    void update(float deltaTime);
    void draw();
}

class SceneManager : ISceneManager
{
    private Dictionary<string, Scene> scenes;
    private Scene? currentScene;


    public static string SCENE_GAME_NAME = "Game";
    public static string SCENE_MENU_NAME = "Menu";


    public SceneManager()
    {
        scenes = new Dictionary<string, Scene>();
        ServiceLoader.RegisterService<ISceneManager>("SceneManager", this);
        scenes.Add(SceneManager.SCENE_MENU_NAME, new MenuScene());
        scenes.Add(SceneManager.SCENE_GAME_NAME, new GameScene());
    }

    public void changeScene(string sceneName, Dictionary<string, object>? parameters = null)
    {
        if (scenes.ContainsKey(sceneName))
        {
            currentScene = scenes[sceneName];

            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }

            currentScene?.init(parameters);
        }
    }

    public void update(float deltaTime)
    {
        if (currentScene != null)
        {
            currentScene?.update(deltaTime);
        }

    }

    public void draw()
    {
        if (currentScene != null)
        {
            currentScene?.draw();
        }
    }

}