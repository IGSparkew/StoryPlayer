using System.Numerics;
using Raylib_cs;

interface IRenderer
{
    public void DrawBoard(GameStateManager gameStateManager);

}

class Renderer : IRenderer
{

    private IResourceManager _resourceManager;
    private Font _defaultFont;

    private Rectangle rectangle;

    private Vector2 descriptionPosition;

    private static int _defaultFontSize = 20;
    

    public Renderer()
    {
        ServiceLoader.RegisterService<Renderer>("Renderer", this);
        _resourceManager = ServiceLoader.GetService<IResourceManager>("ResourceManager");
        _defaultFont = _resourceManager.GetFont("default");

        descriptionPosition = new Vector2(100, 130);
        rectangle = new Rectangle(descriptionPosition, new Vector2(400, descriptionPosition.Y + (Settings.HEIGHT + descriptionPosition.Y)));
    }

    public void DrawBoard(GameStateManager gameStateManager)
    {
        Board? board = gameStateManager.GetCurrentBoard();

        if (board == null) return;

        TextUI titleBoardUi = new TextUI(_defaultFont, _defaultFontSize, board.Name, false, new RenderPosition(RenderConfig.LEFT, RenderConfig.TOP), Color.White);
        TextUI descriptionBoardUi = new TextUI(_defaultFont, _defaultFontSize, board.Description, true, new RenderPosition(RenderConfig.CENTER, RenderConfig.TOP), Color.White, 600);

        titleBoardUi.Margin = new Vector2(10, 10);
        descriptionBoardUi.Margin = new Vector2(-300, 100);

        titleBoardUi.draw();
        descriptionBoardUi.draw();

        List<string> options = board.Connections.Values.ToList<string>();

        foreach (Action action in board.Actions)
        {
            if (action.IsGuided)
            {
                options.Add(action.Name);
            }
        }

        RenderPosition menuPos = new RenderPosition(RenderConfig.CENTER, RenderConfig.CENTER);
        MenuUI menuUI = new MenuUI(menuPos, Color.White, _defaultFont, _defaultFontSize, options, gameStateManager.MenuIndex);
        menuUI.Margin = new Vector2(-300, 100);
        menuUI.draw();
    }
}
