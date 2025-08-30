using System.Numerics;
using Raylib_cs;

class RendererUtils
{
public static Rectangle getBoundsOfText(string text, Vector2 position, Font font, int fontSize, Vector2 margin, float spacing = 1.0f, Vector2? originOverride = null)
{
    // Taille réelle du texte
    Vector2 textSize = Raylib.MeasureTextEx(font, text, fontSize, spacing);

    // Origin utilisé : soit celui passé, soit le centre
    Vector2 origin = originOverride ?? Vector2.Zero;

    // Calcul du coin haut-gauche du rectangle (pivot - origin - margin)
    float x = position.X - origin.X - margin.X;
    float y = position.Y - origin.Y - margin.Y;

    // Largeur et hauteur avec marges
    float width = textSize.X + margin.X * 2;
    float height = textSize.Y + margin.Y * 2;

    return new Rectangle(x, y, width, height);
}


    public static Vector2 CalculateCenterOfText(Font font, string text, int fontSize, float spacing = 1.0f)
    {
        Vector2 size = Raylib.MeasureTextEx(font, text, fontSize, spacing);
        return new Vector2(size.X / 2, size.Y / 2);
    }
    
    public static Vector2 CalculateCenterOfText(TextUI textUI)
    {
        return CalculateCenterOfText(textUI.Font, textUI.Value, textUI.FontSize);
    }

}