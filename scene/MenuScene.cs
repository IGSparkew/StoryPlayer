using System.Numerics;
using IronPython.Runtime;
using Raylib_cs;

class MenuScene : Scene
{
    private Dictionary<string, SettingStory> settingsStories;
    private List<string> stories;
    private int selectedIndex;
    private Font font;

    private MenuUI? menuUI;

    private Vector2 marginMenu;

    private Vector2 selectedPadding;

    private RenderPosition renderPositionMenu;

    private int menuFontSize;


    private TextUI titleUI;
    private int titleFontSize;
    private Vector2 marginTitle;

    private RenderPosition renderPositionTitle;

    private Color color;

    // For logical scene and story
    private Dictionary<string, object> parameters;

    public MenuScene()
    {
        this.selectedIndex = 0;
        this.titleFontSize = 40;
        this.marginMenu = Vector2.Zero;
        this.renderPositionMenu = new RenderPosition(RenderConfig.CENTER, RenderConfig.CENTER);
        this.renderPositionTitle = new RenderPosition(RenderConfig.CENTER, RenderConfig.CENTER);
        this.color = Color.White;
        this.marginTitle = new Vector2(0, -200);
        this.menuFontSize = 20;
        this.selectedPadding = new Vector2(10, 10);

        this.settingsStories = new Dictionary<string, SettingStory>();
        this.stories = new List<string>();
        this.font = ServiceLoader.GetService<IResourceManager>("ResourceManager").GetFont("default");
        this.titleUI = new TextUI(this.font, this.titleFontSize, Settings.GAME_TITLE, false, this.renderPositionTitle, color);
        titleUI.Margin = marginTitle;
        titleUI.Origin = RendererUtils.CalculateCenterOfText(titleUI);
        parameters = new Dictionary<string, object>();
    }


    public override void init(Dictionary<string, object> parameters)
    {
        this.settingsStories = this.storyParser.GetSettingsStories();
        this.renderer.Clear();
        this.stories = this.settingsStories.Keys.ToList();
        this.menuUI = new MenuUI(this.renderPositionMenu, color, font, this.menuFontSize, this.stories, this.marginMenu, this.selectedPadding, true, this.selectedIndex);
        this.renderer.AddElement(this.titleUI);
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
            parameters.Add(SceneManager.SELECTED_STORY, this.stories[this.selectedIndex]);
            this.sceneManager.changeScene(SceneManager.SCENE_GAME_NAME, parameters);
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