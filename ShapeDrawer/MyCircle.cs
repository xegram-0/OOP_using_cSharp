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
        Circle circle = SplashKit.CircleAt(X, Y, Radius);
        SplashKit.FillCircle(Color, circle);
        //SplashKit.FillCircle(Color, X, Y, _radius);
        if (Selected)
        {
            DrawOutline();
        }
    }
    public override void DrawOutline()
    {
        Circle circle = SplashKit.CircleAt(X, Y, Radius+2);
        //SplashKit.DrawCircle(Color.Black, X, Y, _radius+2);
        SplashKit.DrawCircle(Color.Black, circle);
    }
    public override bool IsAt(Point2D point)
    {
        double dx = point.X - X;
        double dy = point.Y - Y;
        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
        return distance <= _radius;
    }

    public override void SaveTo(StreamWriter writer)
    {
        writer.WriteLine("Circle");
        base.SaveTo(writer);
        writer.WriteLine(Radius);
    }
    public override void LoadFrom(StreamReader reader)
    {
        base.LoadFrom(reader);
        Radius = reader.ReadInteger();
    }
}