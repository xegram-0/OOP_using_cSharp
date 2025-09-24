using System.Drawing;
using SplashKitSDK;
using Color = SplashKitSDK.Color;

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
        _color = Color.Chocolate.ToString();
        _x = 0.0f;
        _y = 0.0f;
        _width = param;
        _height = param;
    }

    public void Draw()
    {
        Console.WriteLine("Color is " + _color);
        Console.WriteLine("X is " + _x);
        Console.WriteLine("Y is " + _y);
        Console.WriteLine("Width is " + _width);
        Console.WriteLine("Height is " + _height);
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