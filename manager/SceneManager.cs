interface ISceneManager
{
    void changeScene(string sceneName);
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

    public void changeScene(string sceneName)
    {
        if (scenes.ContainsKey(sceneName))
        {
            currentScene = scenes[sceneName];
            currentScene?.init();
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