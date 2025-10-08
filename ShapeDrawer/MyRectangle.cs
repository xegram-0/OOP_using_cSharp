namespace ShapeDrawer;
using SplashKitSDK;
public class MyRectangle : Shape
{
    private int _width;
    private int _height;
    public MyRectangle(Color color, float x, float y, int width, int height):base(color)
    {
        X = x;
        Y = y;
        _width = width;
        _height = height;
    } 
    public MyRectangle():this(Color.Green, 0, 0, 100, 100){}

    public int Width
    {
        get => _width;
        set => _width = value;
    }
    public int Height
    {
        get => _height;
        set => _height = value;
    }

    public override void Draw()
    {
        SplashKit.FillRectangle(Color, X, Y, Width, Height);
        if (Selected)
        {
            DrawOutline();
        }
    }
    public override void DrawOutline()
    {
        SplashKit.DrawRectangle(Color.Black, X - 2 , Y - 2, Width + 4, Height + 4);
    }

    public override bool IsAt(Point2D point)
    {
        return SplashKit.PointInRectangle(point.X, point.Y, X, Y, _width, _height);
    }
}