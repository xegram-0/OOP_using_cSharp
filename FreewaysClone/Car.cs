using SplashKitSDK;
namespace FreewaysClone;

public class Car
{
    private Road _road;
    private int _currentPointIndex; // Which segment of the road are we on?
    private Point2D _position;
    private float _speed = 2.0f;
    public bool Finished { get; private set; } = false;
    private Color _carColor;

    public Car(Road road)
    {
        _road = road;
        _currentPointIndex = 0;
        _position = _road.PathPoints[0];
            
        // Random color
        _carColor = SplashKit.Rnd(2) == 0 ? Color.Yellow : Color.LightBlue;
    }

    public void Update()
    {
        if (Finished) return;

        // Get target point (the next node in the road list)
        Point2D target = _road.PathPoints[_currentPointIndex + 1];

        // Move towards target
        Vector2D dir = SplashKit.VectorPointToPoint(_position, target);
            
        // Distance check
        if (SplashKit.VectorMagnitude(dir) < _speed)
        {
            // We reached the node, snap to it and increment index
            _position = target;
            _currentPointIndex++;

            // Check if end of road
            if (_currentPointIndex >= _road.PathPoints.Count - 1)
            {
                Finished = true;
            }
        }
        else
        {
            // Move forward
            dir = SplashKit.UnitVector(dir);
            Vector2D velocity = SplashKit.VectorMultiply(dir, _speed);
            _position = Util.AddVectorToPoint(_position, velocity);
        }
    }
    public void Draw()
    {
        if (Finished) return;
            
        // Draw car as a simple circle or rectangle
        SplashKit.FillCircle(_carColor, _position, 5);
    }
}