using System.IO;
using SplashKitSDK;
namespace ShapeDrawer;
public class Drawing
{
    private readonly List<Shape> _shapes;
    private Color _background;
    public Drawing(Color background)
    {
        _shapes = new List<Shape>();
        _background = background; //Not Background
    }
    public Drawing () : this (Color.White){} //Default value
    public Color Background
    {
        get => _background;
        set => _background = value;
    }
    public List<Shape> SelectedShapes
    {
        get
        {
            List<Shape> result = new List<Shape>();
            foreach (Shape shape in _shapes)
            {
                if (shape.Selected)
                {
                    result.Add(shape);
                }
            }
            return result;
        }
    }
    public int ShapeCount => _shapes.Count;
    public void Draw()
    {
        SplashKit.ClearScreen(_background);
        foreach (Shape shape in _shapes)
        {
            shape.Draw();
        }
    }
    public void SelectShapesAt(Point2D position)
    {
        foreach (Shape shape in _shapes)
        {
            if (shape.IsAt(SplashKit.MousePosition()))
            {
                shape.Selected = true;
            }
        }
    }
    public void AddShape(Shape shape)
    {
        _shapes.Add(shape);
    }
    public void RemoveShape(Shape shape)
    {
        _shapes.Remove(shape);
    }

    public void Save(string filename)
    {
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            writer.WriteColor(Background);
            writer.WriteLine(ShapeCount);
            foreach (Shape shape in _shapes)
            {
                shape.SaveTo(writer);
            }
        }
        finally{ writer.Close();}
    }
}