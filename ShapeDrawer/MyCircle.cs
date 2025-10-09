namespace ShapeDrawer;
using SplashKitSDK;

public class MyCircle : Shape
{
    private int _radius;
    public MyCircle() : this(Color.Blue, 50) {}
    public MyCircle(Color color, int radius) : base(color)
    {
        _radius = radius;
    }
    public int Radius
    {
        get => _radius;
        set => _radius = value;
    }
    public override void Draw()
    {
        SplashKit.FillCircle(Color, X, Y, _radius);
        if (Selected)
        {
            DrawOutline();
        }
    }
    public override void DrawOutline()
    {
        SplashKit.DrawCircle(Color.Black, X, Y, _radius+2);
    }
    public override bool IsAt(Point2D point)
    {
        double dx = point.X - X;
        double dy = point.Y - Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        return distance <= _radius;
    }
}