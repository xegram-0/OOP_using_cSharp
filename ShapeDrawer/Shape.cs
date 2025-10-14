using SplashKitSDK;
namespace ShapeDrawer;
public abstract class Shape
{
    private Color _color;
    private float _x, _y;
    private bool _selected; //False by default
    protected Shape(Color color) //Constructor Shape takes color as parameter
    {
        _color = color;
        _x = 0.0f;
        _y = 0.0f;
    }
    protected Shape():this(Color.Yellow) {} //Default value
    public bool Selected 
    {
        get => _selected;
        set => _selected = value;
    } 
    public Color Color
    {
        get => _color; 
        set => _color = value; 
    }
    public float X
    {
        get => _x;
        set => _x = value;
    }
    public float Y
    {
        get => _y;
        set => _y = value;
    }
    public abstract void Draw();
    public abstract bool IsAt(Point2D point);
    public abstract void DrawOutline();

    public virtual void SaveTo(StreamWriter writer)
    {
        writer.WriteColor(Color);
        writer.WriteLine(X);
        writer.WriteLine(Y);
    }
}