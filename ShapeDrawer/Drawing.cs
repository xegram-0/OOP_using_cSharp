using System.IO;
using System.Net.Http.Headers;
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

    public void Load(string filename)
    {
        StreamReader reader = new StreamReader(filename);
        try
        {
            Background = reader.ReadColor();
            int count = reader.ReadInteger();
            _shapes.Clear();
            for (int i = 0; i < count; i++)
            {
                Shape shape;
                string kind = reader.ReadLine()!;
                switch (kind)
                {
                    case "Circle":
                        shape = new MyCircle();
                        break;
                    case "Rectangle":
                        shape = new MyRectangle();
                        break;
                    case "Line":
                        shape = new MyLine();
                        break;
                    default:
                        throw new InvalidDataException("Unknown shape kind: " + kind);
                        continue;
                }
                shape.LoadFrom(reader);
                AddShape(shape);
            }
        }
        finally{ reader.Close();}
    }
}