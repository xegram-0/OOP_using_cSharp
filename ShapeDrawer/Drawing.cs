using SplashKitSDK;

namespace ShapeDrawer;

public class Drawing
{
    private readonly List<Shape> _shapes;
    private Color _background;
    
    public Color Background
    {
        get => _background;
        set => _background = value;
    }

    public Drawing(Color background)
    {
        _shapes = new List<Shape>();
        _background = Background;
    }
    public Drawing () : this (Color.White)
    {
        
    }
    
    public int ShapeCount => _shapes.Count;
    
    public void AddShape(Shape shape)
    {
        _shapes.Add(shape);
    }

    public void RemoveShape(Shape shape)
    {
        _=_shapes.Remove(shape);
    }

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
            shape.Selected = shape.IsAt((int)position.X, (int)position.Y);
        }
    }
    public List<Shape> SelectedShapes
    {
        get
        {
            var result = new List<Shape>();
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
}