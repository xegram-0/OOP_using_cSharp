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
        //try use using, it is better
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            //save the background color with how many shapes are drawn
            //Save() would call SaveTo() of the Shape class to write to the text file using 'writer' as anchor
            writer.WriteColor(Background); // first 3 lines
            writer.WriteLine(ShapeCount); // 4th line 
            foreach (Shape shape in _shapes)
            {
                shape.SaveTo(writer);
            }
        }
        finally{ writer.Close();} //closing file like that in python
    }

    public void Load(string filename)
    {
        StreamReader reader = new StreamReader(filename);
        try
        {
            Background = reader.ReadColor();
            int count = reader.ReadInteger();
            _shapes.Clear(); //clear previously saved shapes in _shapes
            for (int i = 0; i < count; i++)
            {
                Shape shape;
                string kind = reader.ReadLine();
                switch (kind)
                //read the file, see the shape name, create the shape obj
                //using Shape class LoadFrom() to load the shape based on the X,Y and its shape other attributes
                //save the shape to the _shape List of Shape obj
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
                        throw new InvalidDataException($"Shape kind {kind} not supported");
                        continue;
                }
                shape.LoadFrom(reader);
                AddShape(shape);
            }
        }
        finally{ reader.Close();}
    }
}