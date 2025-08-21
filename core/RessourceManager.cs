using Raylib_cs;

interface IResourceManager
{
    void LoadTexture(string name, string path);
    void LoadFont(string name, string path);
    Texture2D GetTexture(string name);
    Font GetFont(string name);

    void UnloadAllResources();
}


class RessourceManager : IResourceManager
{

    public static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();
    public static Dictionary<String, Font> fonts = new Dictionary<string, Font>();

    public RessourceManager()
    {
        // Initialize resource manager if needed
        ServiceLoader.RegisterService<IResourceManager>("ResourceManager", this);
    }


    public void LoadTexture(string name, string path)
    {
        if (textures.ContainsKey(name))
        {
            return; // Texture already loaded
        }

        textures.Add(name, Raylib.LoadTexture(path));
    }

    public void LoadFont(string name, string path)
    {
        if (fonts.ContainsKey(name))
        {
            return; // Font already loaded
        }

        fonts.Add(name, Raylib.LoadFont(path));
    }

    public Texture2D GetTexture(string name)
    {
        return textures.ContainsKey(name) ? textures[name] : throw new KeyNotFoundException($"Texture '{name}' not found.");
    }

    public Font GetFont(string name)
    {
        return fonts.ContainsKey(name) ? fonts[name] : throw new KeyNotFoundException($"Font '{name}' not found.");
    }

    public void UnloadAllResources()
    {
        foreach (var texture in textures.Values)
        {
            Raylib.UnloadTexture(texture);
        }
        textures.Clear();

        foreach (var font in fonts.Values)
        {
            Raylib.UnloadFont(font);
        }
        fonts.Clear();
    }
}