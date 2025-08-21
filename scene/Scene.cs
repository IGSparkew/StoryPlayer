abstract class Scene {

    protected ISceneManager sceneManager;
    protected Renderer renderer;
    protected GameStateManager gameStateManager;
    protected IScriptReader scriptReader;

    protected IStoryParser storyParser;



    public Scene()
    {
        sceneManager = ServiceLoader.GetService<ISceneManager>("SceneManager");
        renderer = ServiceLoader.GetService<Renderer>("Renderer");
        scriptReader = ServiceLoader.GetService<IScriptReader>("ScriptReader");
        gameStateManager = new GameStateManager();
        storyParser = ServiceLoader.GetService<IStoryParser>("StoryParser");
    }

    public abstract void init();
    public abstract void update(float dt);
    public abstract void draw();

}