using System.Numerics;

public class RenderPosition
{
    public Vector2 Position { get; set; }

    private Vector2 initPosition;

    private RenderConfig renderConfigX;

    private RenderConfig renderConfigY;

    private static float CENTER_WIDTH = Settings.WIDTH / 2;
    private static float CENTER_HEIGHT = Settings.HEIGHT / 2;


    public RenderPosition(RenderConfig renderConfigX, RenderConfig renderConfigY)
    {
        if (renderConfigX == RenderConfig.TOP || renderConfigX == RenderConfig.DOWN || renderConfigY == RenderConfig.RIGHT || renderConfigY == RenderConfig.LEFT)
        {
            throw new Exception("can't create Render position with this render configs, X: " + renderConfigX.ToString() + " Y: " + renderConfigY.ToString());
        }

        float x = 0.0f;
        float y = 0.0f;

        if (renderConfigX == RenderConfig.CENTER)
        {
            x = CENTER_WIDTH;
        }

        if (renderConfigX == RenderConfig.RIGHT)
        {
            x = Settings.WIDTH;
        }

        if (renderConfigY == RenderConfig.CENTER)
        {
            y = CENTER_HEIGHT;
        }

        if (renderConfigY == RenderConfig.DOWN)
        {
            y = Settings.HEIGHT;
        }

        Position = new Vector2(x, y);
        this.initPosition = Position;
        this.renderConfigX = renderConfigX;
        this.renderConfigY = renderConfigY;
    }

    public void AddGap(float _g, bool inWidth)
    {
        float nX = Position.X;
        float nY = Position.Y;

        if (inWidth)
        {
            nX += _g;
        }
        else
        {
            nY += _g;
        }

        this.Position = new Vector2(nX, nY);
    }

    public void resetGap()
    {
        this.Position = initPosition;
    }

    public RenderPosition Clone()
    {
        RenderPosition renderPosition = RenderPosition._default();
        renderPosition.Position = new Vector2(this.Position.X, this.Position.Y);
        return renderPosition;
    }

    public static RenderPosition _default()
    {
        return new RenderPosition(RenderConfig.LEFT, RenderConfig.TOP);
    }

    public override string ToString()
    {
        return "X:" + Position.X.ToString() + " Y: " + Position.Y.ToString();
    }

}