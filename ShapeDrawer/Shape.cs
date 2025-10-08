using SplashKitSDK;
namespace ShapeDrawer;

public abstract class Shape
{
    private Color _color;
    private float _x, _y;
    private bool _selected;
    protected Shape(Color color)
    {
        _color = color;
        _x = 0.0f;
        _y = 0.0f;
    }

    protected Shape():this(Color.Yellow) {}
    public bool Selected //False by default
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
    public virtual void Draw(){}
    public virtual bool IsAt(Point2D point)
    {
        return false;
    }
    public virtual void DrawOutline(){}
}