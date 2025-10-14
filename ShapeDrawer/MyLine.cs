namespace ShapeDrawer;
using SplashKitSDK;
public class MyLine : Shape
{
    private float _xLine, _yLine;
    private MyLine(Color color) : base(color)
    {
        //Position of the first point of the line
        _xLine = SplashKit.MouseX();
        _yLine = SplashKit.MouseY();
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
        //SplashKit.DrawLine(Color, X ,Y, _xLine, _yLine);
        for (int i = 0; i < 10; i++)
        {
            SplashKit.DrawLine(Color, _xLine  , _yLine+i*20, X +100, Y+i*20);
        }
        if (Selected)
        {
            DrawOutline();
        }
    }
    public override void DrawOutline()
    {
        SplashKit.DrawCircle(Color.Black, X+100, Y, 6);
        SplashKit.DrawCircle(Color.Black, _xLine, _yLine, 6);
    }
    public override bool IsAt(Point2D point)
    {
        for (int i = 0; i < 10; i++)
        {
            Line line = SplashKit.LineFrom(_xLine,_yLine + i * 20,_xLine + 100,_yLine + i * 20);
            if (SplashKit.PointOnLine(point, line, 5))
            {
                return true;
            }
        }
        return false;
        //PointOnLine requires line only so create a line to check
        //Line line = SplashKit.LineFrom(X, Y, _xLine, _yLine);
        //return SplashKit.PointOnLine(point, line, 10);
    }

    public override void SaveTo(StreamWriter writer)
    {
        writer.WriteLine("Line");
        base.SaveTo(writer);
        writer.WriteLine(XLine);
        writer.WriteLine(YLine);
    }
}