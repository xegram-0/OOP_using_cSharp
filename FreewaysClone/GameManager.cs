using SplashKitSDK;
namespace FreewaysClone;

public class GameManager
{
     private List<Road> _roads;
        private List<Car> _cars;
        private List<Point2D> _currentDrawingPoints; 
        private bool _isDrawing;
        private const float DRAW_SMOOTHING = 10.0f; 

        public GameManager()
        {
            _roads = new List<Road>();
            _cars = new List<Car>();
            _currentDrawingPoints = new List<Point2D>();
            _isDrawing = false;
        }

        public void HandleInput()
        {
            Point2D mousePos = SplashKit.MousePosition();

            // 1. Start Drawing
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _isDrawing = true;
                _currentDrawingPoints.Clear();
                _currentDrawingPoints.Add(mousePos);
            }

            // 2. Continue Drawing (Smoothing)
            if (_isDrawing && SplashKit.MouseDown(MouseButton.LeftButton))
            {
                Point2D lastPoint = _currentDrawingPoints[_currentDrawingPoints.Count - 1];
                // Only add point if moved far enough
                if (SplashKit.PointPointDistance(lastPoint, mousePos) > DRAW_SMOOTHING)
                {
                    _currentDrawingPoints.Add(mousePos);
                }
            }

            // 3. Finish Drawing
            if (_isDrawing && SplashKit.MouseUp(MouseButton.LeftButton))
            {
                _isDrawing = false;
                
                // FIXED: Changed > 2 to > 1. Now allows 2-point roads (straight lines).
                if (_currentDrawingPoints.Count > 1)
                {
                    Road newRoad = new Road(new List<Point2D>(_currentDrawingPoints));
                    _roads.Add(newRoad);
                    _cars.Add(new Car(newRoad));
                }
            }
        }

        public void Update()
        {
            // Spawn random cars
            if (SplashKit.Rnd(100) < 2 && _roads.Count > 0)
            {
                Road r = _roads[SplashKit.Rnd(_roads.Count)];
                _cars.Add(new Car(r));
            }

            foreach (var car in _cars)
            {
                car.Update();
            }
            
            _cars.RemoveAll(c => c.Finished);
        }

        public void Draw()
        {
            // Draw completed roads
            foreach (var road in _roads) road.Draw();

            // Draw drawing preview
            if (_isDrawing)
            {
                // FIXED: Draw the start point immediately so user sees feedback
                if (_currentDrawingPoints.Count > 0)
                {
                    SplashKit.FillCircle(Color.White, _currentDrawingPoints[0], 5);
                }

                // Draw the line being dragged
                if (_currentDrawingPoints.Count > 1)
                {
                    for (int i = 0; i < _currentDrawingPoints.Count - 1; i++)
                    {
                        SplashKit.DrawLine(Color.White, _currentDrawingPoints[i], _currentDrawingPoints[i + 1], SplashKit.OptionLineWidth(2));
                    }
                }
            }

            // Draw cars
            foreach (var car in _cars) car.Draw();
        }
}