using SplashKitSDK;

namespace FreewaysGame
{
    public class Car
    {
        private Road _assignedRoad;
        private int _currentNodeIndex;
        private Point2D _position;
        private float _speed;
        private Color _color;
        
        public bool IsFinished { get; private set; } = false;

        public Car(Road road)
        {
            _assignedRoad = road;
            _currentNodeIndex = 0;
            _speed = GameConfig.CarSpeedDefault;

            // Randomize car appearance
            _color = SplashKit.Rnd(2) == 0 ? Color.Yellow : Color.LightBlue;

            // Initialize position
            if (_assignedRoad.PathPoints.Count > 0)
            {
                _position = _assignedRoad.PathPoints[0];
            }
            else
            {
                IsFinished = true;
            }
        }

        public void Update()
        {
            if (IsFinished) return;
            if (_assignedRoad.PathPoints.Count < 2) return;

            // 1. Determine target
            // Ensure we don't go out of bounds
            if (_currentNodeIndex + 1 >= _assignedRoad.PathPoints.Count)
            {
                IsFinished = true;
                return;
            }

            Point2D target = _assignedRoad.PathPoints[_currentNodeIndex + 1];
            Vector2D dirToTarget = SplashKit.VectorPointToPoint(_position, target);
            float distToTarget = (float)SplashKit.VectorMagnitude(dirToTarget);

            // 2. Move
            if (distToTarget < _speed)
            {
                // We reached the node, snap to it and target the next one
                _position = target;
                _currentNodeIndex++;
            }
            else
            {
                // Move towards node
                Vector2D moveDir = Utilities.SafeUnitVector(dirToTarget);
                Vector2D velocity = SplashKit.VectorMultiply(moveDir, _speed);
                _position = Utilities.AddVectorToPoint(_position, velocity);
            }
        }

        public void Draw()
        {
            if (IsFinished) return;
            
            // Draw a simple car shape
            SplashKit.FillCircle(_color, _position, 6); // Car body
            SplashKit.FillCircle(Color.Black, _position, 2); // "Roof" detail
        }
    }
}