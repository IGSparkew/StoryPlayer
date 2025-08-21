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

        TextUI titleBoardUi = new TextUI(_defaultFont, _defaultFontSize, board.Name, false, new RenderPosition(RenderConfig.CENTER, RenderConfig.TOP), Color.White);
        TextUI descriptionBoardUi = new TextUI(_defaultFont, _defaultFontSize, board.Description, true, new RenderPosition(RenderConfig.CENTER, RenderConfig.CENTER), Color.White, 600);

        drawText(titleBoardUi);
        drawText(descriptionBoardUi);

        List<string> options = board.Connections.Values.ToList<string>();

        foreach (Action action in board.Actions)
        {
            if (action.IsGuided)
            {
                options.Add(action.Name);
            }
        }

        drawMenu(options, _defaultFontSize, gameStateManager.MenuIndex);
    }

    private void drawMenu(List<string> connections, int fontSize, int selected)
    {
        foreach (var connection in connections)
        {
            Vector2 position = new Vector2(50, 400 + connections.IndexOf(connection) * (fontSize + 20));

            if (selected == connections.IndexOf(connection))
            {
                Vector2 textSize = Raylib.MeasureTextEx(_defaultFont, connection, fontSize, 1.0f);

                // Marges configurables
                int marginX = 10;
                int marginY = 10;

                Rectangle rect = new Rectangle(
                    position.X - marginX,
                    position.Y - marginY,
                    textSize.X + (marginX * 2),
                    textSize.Y + (marginY * 2)
                );

                Raylib.DrawRectangleLinesEx(rect, 1.0f, Color.White);
            }

            drawText(connection, position, _defaultFont, fontSize);
        }
    }

    private void drawText(TextUI textUI)
    {

        if (textUI.IsWrapped)
        {
            drawWrappedText(textUI.Value, textUI.Position.Position, textUI.MaxWidth, textUI.Font, textUI.FontSize);
        }
        else
        {
            Raylib.DrawTextEx(textUI.Font, textUI.Value, textUI.Position.Position, textUI.FontSize, 1.0f, textUI.Color);
        }

    }

    private void drawText(string text, Vector2 position, Font font, int fontSize)
    {
        Raylib.DrawTextEx(font, text, position, fontSize, 1.0f, Color.White);
    }
    
    private void drawWrappedText(string text, Vector2 position, float maxWidth, Font font, int fontSize)
    {

        var words = text.Split(' ');
        string line = "";
        int lineHeight = fontSize + 4;

        foreach (var word in words)
        {
            string candidate = (line.Length == 0) ? word : line + " " + word;
            int width = Raylib.MeasureText(candidate, fontSize);

            if (width > maxWidth)
            {
                // If the word is too wide, draw the current line and start a new one
                Raylib.DrawTextEx(font, line, position, fontSize, 1.0f, Color.White);
                position.Y += lineHeight;
                line = word;
            }
            else
            {
                line = candidate;
            }
        }

        // Draw the last line
        if (line.Length > 0) Raylib.DrawTextEx(font, line, position, fontSize, 1.0f, Color.White);
    }
}
