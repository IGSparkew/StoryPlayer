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

    public SceneManager()
    {
        scenes = new Dictionary<string, Scene>();
        ServiceLoader.RegisterService<ISceneManager>("SceneManager", this);
        scenes.Add("Menu", new MenuScene());
        scenes.Add("Game", new GameScene());
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