using SplashKitSDK;

//THIS SHOULD BE REMOVED
//using Color = System.Drawing.Color;

namespace ShapeDrawer;

public class Shape
{
    private Color _color;
    private float _x;
    private float _y;
    private int _width;
    private int _height;

    public Shape(int param)
    {
        _color = Color.Chocolate;
        _x = 0.0f;
        _y = 0.0f;
        _width = param;
        _height = param;
    }

    /*
     * Expression-bodied property
     * Shorthand for get and set
     */
    
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
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

    public void Draw()
    {
        SplashKit.FillRectangle(_color,_x ,_y, _width, _height);
    }

    public float ComputeArea()
    {
        float result = ((_width) * (_height));
        return result;
    }
    public bool IsAt(int xInput, int yInput)
    {
        if (xInput >= _x && xInput <= _x + _width && yInput >= _y && yInput <= _y + _height)
        {
            Console.WriteLine("True");
            return true;
        }
        else
        {
            Console.WriteLine("False");
            return false;
        }
    }
}