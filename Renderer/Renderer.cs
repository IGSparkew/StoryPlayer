using System.Numerics;
using Raylib_cs;

interface IRenderer
{
    void AddElement(UIElement element);
    void Clear();
    void setCurrentRendererBoard(GameStateManager gameStateManager);
    void Update(GameStateManager gameStateManager);
    void Draw();

}

class Renderer : IRenderer
{

    private IResourceManager _resourceManager;
    private Font _defaultFont;
    private static int _defaultFontSize = 20;

    List<UIElement> elements;

    public Renderer()
    {
        elements = new List<UIElement>();
        ServiceLoader.RegisterService<Renderer>("Renderer", this);
        _resourceManager = ServiceLoader.GetService<IResourceManager>("ResourceManager");
        _defaultFont = _resourceManager.GetFont("default");
    }

    public void Clear()
    {
        elements.Clear();
    }

    public void AddElement(UIElement element)
    {
        this.elements.Add(element);
    }

    public void setCurrentRendererBoard(GameStateManager gameStateManager)
    {

        Board? board = gameStateManager.GetCurrentBoard();
        SettingStory? settingStory = gameStateManager.SettingStory;

        if (board == null || settingStory == null)
        {
            throw new Exception("Error can't load story");
        }


        Clear();
        List<LayoutConfig> configs = settingStory.UIelements;

        if (configs.Count == 0)
        {
            throw new Exception("Error no config founds for render this story with Path: " + settingStory.Path);
        }


        foreach (LayoutConfig config in configs)
        {
            switch (config.Type)
            {
                case "TextUI":
                    addTextUIElement(config, board);
                    break;
                case "MenuUI":
                    addMenuUIElement(config, board, gameStateManager);
                    break;
                default:
                    throw new Exception("can't find this type of ui element: " + config.Type);
            }
        }
    }

    private void addTextUIElement(LayoutConfig config, Board board)
    {
        // change layout config to setup more parameters like font
        LayoutConfigResolver resolver = LayoutConfigResolver.resolver(config, board);
        RenderPosition position = PositionConfigResolver.resolvePosition(config.Anchor);

        TextUI textUI = new TextUI(_defaultFont, _defaultFontSize, resolver.getValue(), config.IsWrapped, position, Color.White, config.MaxWidth);
        textUI.Margin = config.GetMargin();
        AddElement(textUI);
    }

    private void addMenuUIElement(LayoutConfig config, Board board, GameStateManager gameStateManager)
    {
        List<string> options = board.Connections.Values.ToList<string>();
        foreach (Action action in board.Actions)
        {
            if (action.IsGuided)
            {
                options.Add(action.Name);
            }
        }

        RenderPosition position = PositionConfigResolver.resolvePosition(config.Anchor);
        //TODO: make padding in LayoutConfig
        MenuUI menuUI = new MenuUI(position, Color.White, _defaultFont, _defaultFontSize, options, config.GetMargin(), new Vector2(10, 10),gameStateManager.MenuIndex);
        menuUI.Margin = config.GetMargin();
        AddElement(menuUI);
    }

    public void Update(GameStateManager gameStateManager)
    {
        if (gameStateManager.IsUpdatedBoard)
        {
            setCurrentRendererBoard(gameStateManager);
        }

        foreach (UIElement element in elements)
        {
            element.update(gameStateManager);
        }
    }

    public void Draw()
    {
        foreach (UIElement element in elements)
        {
            element.draw();
        }
    }
}
