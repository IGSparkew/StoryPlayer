using System.Numerics;
using IronPython.Runtime;
using Raylib_cs;

class MenuScene : Scene
{
    private Dictionary<string, SettingStory> settingsStories;
    private List<string> stories;
    private int selectedIndex;

    private MenuUI? menuUI;

    private Font font;


    // For logical scene and story
    private Dictionary<string, object> parameters;

    public MenuScene()
    {
        this.settingsStories = new Dictionary<string, SettingStory>();
        this.stories = new List<string>();
        this.selectedIndex = 0;
        this.font = ServiceLoader.GetService<IResourceManager>("ResourceManager").GetFont("default");
        parameters = new Dictionary<string, object>();
    }


    public override void init(Dictionary<string, object> parameters)
    {
        this.settingsStories = this.storyParser.GetSettingsStories();
        this.renderer.Clear();
        this.stories = this.settingsStories.Keys.ToList();
        this.menuUI = new MenuUI(new RenderPosition(RenderConfig.CENTER, RenderConfig.CENTER), Color.White, font, 20, this.stories, new Vector2(-70, 0), new Vector2(10, 10), this.selectedIndex);
        this.renderer.AddElement(this.menuUI);
    }

    public override void update(float dt)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            this.selectedIndex = (this.selectedIndex + 1) % this.stories.Count;
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            this.selectedIndex = (this.selectedIndex - 1 + this.stories.Count) % this.stories.Count;
        } else if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            parameters.Add("selected_story", this.stories[this.selectedIndex]);
            this.sceneManager.changeScene("Game", parameters);
        }

        if (this.menuUI != null)
        {
            this.menuUI.Selected = this.selectedIndex;
        }
    }
    
    public override void draw()
    {
        this.renderer.Draw();
    }
}