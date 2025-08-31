public class EventManager
{

    private GameStateManager gameStateManager;
    private IRenderer renderer;
    private IScriptReader scriptReader;

    public EventManager(GameStateManager gmsm)
    {
        gameStateManager = gmsm;
        renderer = ServiceLoader.GetService<IRenderer>("Renderer");
        scriptReader = ServiceLoader.GetService<IScriptReader>("ScriptReader");
    }

    public void TriggerEvent(Event evt)
    {
        switch (evt.Type)
        {
            case EventType.VIEW:
                HandleView(evt);
                break;
            case EventType.BLOCKED:
                HandleBlocked(evt);
                break;
            case EventType.CUSTOM:
                HandleCustom(evt);
                break;
            case EventType.MOVE:
                HandleMove(evt);
                break;
            case EventType.NONE:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private MenuUI GetMenuUI()
    {
        SettingStory? settingStory = gameStateManager.SettingStory;
        if (settingStory == null)
        {
            throw new Exception("Error no setting story found");
        }

        string elementId = "";

        settingStory.UIelements.ForEach(config =>
        {
            if (config.Type == LayoutConfigType.MenuUI)
            {
                elementId = config.Id;
            }
        });

        if (elementId == "")
        {
            throw new Exception("Error no menu ui found");
        }

        MenuUI menu = (MenuUI)this.renderer.GetUiElement(elementId);
        return menu;
    }

    private void HandleView(Event evt)
    {
        // Handle view event
        MenuUI menu = GetMenuUI();

        foreach (var option in menu.Options)
        {
            if (option.Id == evt.Output)
            {
                option.IsVisible = !option.IsVisible;
                break;
            }
        }
    }

    private void HandleBlocked(Event evt)
    {
        // Handle blocked event
        MenuUI menu = GetMenuUI();
        menu.blockedOption(evt.Output);
    }

    private void HandleCustom(Event evt)
    {
        // Handle custom event
        if (string.IsNullOrEmpty(evt.Script))
        {
            throw new Exception("Error no script found for custom event");
        }
        // TODO add args later
        gameStateManager.logs.Add(scriptReader.run(evt.Script, gameStateManager, false, null));
    }

    private void HandleMove(Event evt)
    {
        // Handle move event
        if (this.gameStateManager.BoardManager.HasBoard(evt.Output))
        {
            this.gameStateManager.SetCurrentBoard(evt.Output);
        }
        else
        {
            throw new Exception("Error no board found with name: " + evt.Output);
        }
    }


}