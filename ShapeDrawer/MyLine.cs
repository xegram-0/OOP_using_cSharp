namespace ShapeDrawer;
using SplashKitSDK;
public class MyLine : Shape
{
    private float _xLine, _yLine;
    private MyLine(Color color) : base(color)
    {
        //Position of the first point of the line
        _xLine = X + 50;
        _yLine = Y + 50;
    }
    public MyLine():this(Color.Red) {}
    public float XLine
    {
        get => _xLine;
        set => _xLine = value; 
    }
    public float YLine
    {
        get => _yLine; 
        set => _yLine = value; 
    }
    public override void Draw()
    {
        SplashKit.DrawLine(Color, X ,Y, _xLine, _yLine);
        if (Selected)
        {
            DrawOutline();
        }
    }
    public override void DrawOutline()
    {
        SplashKit.DrawCircle(Color.Black, X, Y, 6);
        SplashKit.DrawCircle(Color.Black, _xLine, _yLine, 6);
    }
    public override bool IsAt(Point2D point)
    {
        //PointOnLine requires line only so create a line to check
        Line line = SplashKit.LineFrom(X, Y, _xLine, _yLine);
        return SplashKit.PointOnLine(point, line, 10);
    }
    
}