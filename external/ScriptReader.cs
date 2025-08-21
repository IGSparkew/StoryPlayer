using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
public interface IScriptReader
{
    string run(string fileName, GameStateManager gameStateManager, bool isGlobal=true, Dictionary<string, string>? args = null);
}


public class ScriptReader : IScriptReader
{
    private readonly Microsoft.Scripting.Hosting.ScriptEngine _py;

    public ScriptReader()
    {
        _py = IronPython.Hosting.Python.CreateEngine();
        ServiceLoader.RegisterService<IScriptReader>("ScriptReader", this);
    }

    public string run(String fileName, GameStateManager gameStateManager, bool isGlobal=true, Dictionary<string, string>? args = null)
    {

        string path = "";
        string scriptName = fileName + ".py";

        if (isGlobal)
        {
            path = Path.Combine(Settings.SCRIPT_PATH, scriptName);
        }
        else
        {

            if (gameStateManager.SettingStory == null)
            {
                throw new Exception("Error settings can't be null when you execute story script!");
            }

            path = Path.Combine(gameStateManager.SettingStory.Path, "scripts", scriptName);
        }

        ScriptApi api = new ScriptApi(gameStateManager);
        var scope = _py.CreateScope();

        scope.SetVariable("api", api);
        scope.SetVariable("args", args);

        String code = File.ReadAllText(path);

        _py.Execute(code, scope);

        if (scope.ContainsVariable("execute"))
        {
            dynamic execute = scope.GetVariable("execute");
            return execute(api, args);
        }

        return scope.GetVariable<string>("result");
    }
}