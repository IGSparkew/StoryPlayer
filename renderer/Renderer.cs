using System.Numerics;
using Raylib_cs;

interface IRenderer
{
    void AddElement(UIElement element);
    void Clear();
    void setCurrentRendererBoard(GameStateManager gameStateManager);
    void Update(GameStateManager gameStateManager);
    UIElement GetUiElement(string id);
    void Draw();

}

class Renderer : IRenderer
{

    private IResourceManager _resourceManager;
    private Font _defaultFont;
    private static int _defaultFontSize = 20;

    Dictionary<string, UIElement> elements;

    public Renderer()
    {
        elements = new Dictionary<string, UIElement>();
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
        this.elements.Add(element.Id, element);
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
                case LayoutConfigType.TextUI:
                    addTextUIElement(config, board);
                    break;
                case LayoutConfigType.MenuUI:
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

        TextUI textUI = new TextUI(_defaultFont, _defaultFontSize, resolver.getValue(), config.IsWrapped, config.Id, position, Color.White, config.MaxWidth);
        textUI.Margin = config.GetMargin();
        AddElement(textUI);
    }

    private void addMenuUIElement(LayoutConfig config, Board board, GameStateManager gameStateManager)
    {
        Dictionary<string, string> options = board.Connections;
        foreach (Action action in board.Actions)
        {
            if (action.Show)
            {
                options.Add(action.Name, action.Description);
            }
        }

        RenderPosition position = PositionConfigResolver.resolvePosition(config.Anchor);
        //TODO: make padding in LayoutConfig
        MenuUI menuUI = new MenuUI(config.Id, position, Color.White, _defaultFont, _defaultFontSize, options, config.GetMargin(), new Vector2(10, 10), false, gameStateManager.MenuIndex);
        menuUI.Margin = config.GetMargin();
        foreach (var evt in board.Events)
        {
            Console.WriteLine($"{evt.Type} - {evt.TypeOutput} - {evt.Output} - {evt.Default}");
            if (evt.Type == EventType.VIEW && (evt.TypeOutput == EventTypeOutput.CONNECTOR || evt.TypeOutput == EventTypeOutput.ACTION) && evt.Default == "HIDE")
            {
                menuUI.renderOption(evt.Output, false);
            }
            else if (evt.Type == EventType.BLOCKED && (evt.TypeOutput == EventTypeOutput.CONNECTOR || evt.TypeOutput == EventTypeOutput.ACTION) && evt.Default == "BLOCK")
            {
                menuUI.blockedOption(evt.Output);
            }
        }

        AddElement(menuUI);
    }

    public void Update(GameStateManager gameStateManager)
    {
        if (gameStateManager.IsUpdatedBoard)
        {
            setCurrentRendererBoard(gameStateManager);
        }

        foreach (var element in elements)
        {
            element.Value.update(gameStateManager);
        }
    }

    public UIElement GetUiElement(string id)
    {
        if (elements.TryGetValue(id, out var element))
        {
            return element;
        }
        else
        {
            throw new KeyNotFoundException($"UI Element with ID '{id}' not found.");
        }
    }

    public void Draw()
    {
        foreach (var element in elements)
        {
            element.Value.draw();
        }
    }
}
