
public class PositionConfigResolver
{
    public static RenderPosition resolvePosition(string anchor)
    {
        if (string.IsNullOrEmpty(anchor))
        {
            throw new Exception("can't convert empty anchor");
        }

        string[] splitted = anchor.Trim().Split("-");
        string x = splitted[0].ToUpper();
        string y = splitted[1].ToUpper();

        if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
        {
            throw new Exception("can't convert to position for this anchor:" + anchor);
        }

        RenderConfig renderConfigHorizontal = Enum.Parse<RenderConfig>(x);
        RenderConfig renderConfigVertical = Enum.Parse<RenderConfig>(y);

        return new RenderPosition(renderConfigHorizontal, renderConfigVertical);
    }

}