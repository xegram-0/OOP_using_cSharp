using System.Collections.Generic;
using SplashKitSDK;

namespace FreewaysGame
{
    public class FreewaysApp
    {
        private Window _window;
        private List<Road> _roads;
        private List<Car> _cars;
        
        // Drawing State
        private bool _isDrawing;
        private List<Point2D> _tempPoints; // Points currently being drawn

        public FreewaysApp()
        {
            _window = new Window(GameConfig.WindowTitle, GameConfig.ScreenWidth, GameConfig.ScreenHeight);
            _roads = new List<Road>();
            _cars = new List<Car>();
            _tempPoints = new List<Point2D>();
            _isDrawing = false;
        }

        public void Run()
        {
            while (!_window.CloseRequested)
            {
                SplashKit.ProcessEvents();
                
                HandleInput();
                UpdateGame();
                RenderGame();
                
                SplashKit.RefreshScreen(GameConfig.TargetFPS);
            }
        }

        private void HandleInput()
        {
            Point2D mousePos = SplashKit.MousePosition();

            // Mouse Down: Start Drawing
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                _isDrawing = true;
                _tempPoints.Clear();
                _tempPoints.Add(mousePos);
            }

            // Mouse Drag: Add points
            if (_isDrawing && SplashKit.MouseDown(MouseButton.LeftButton))
            {
                Point2D lastPoint = _tempPoints[_tempPoints.Count - 1];
                // Only add point if we moved far enough (Smoothing)
                if (SplashKit.PointPointDistance(lastPoint, mousePos) > GameConfig.DrawSmoothing)
                {
                    _tempPoints.Add(mousePos);
                }
            }

            // Mouse Up: Finalize Road
            if (_isDrawing && SplashKit.MouseUp(MouseButton.LeftButton))
            {
                _isDrawing = false;

                // Only create road if it has enough points (Start + End)
                if (_tempPoints.Count > 1)
                {
                    Road newRoad = new Road(_tempPoints);
                    _roads.Add(newRoad);
                    
                    // Instant feedback: Spawn a car on the new road
                    _cars.Add(new Car(newRoad));
                }
            }
        }

        private void UpdateGame()
        {
            // Randomly spawn cars
            if (_roads.Count > 0 && SplashKit.Rnd(100) < GameConfig.CarSpawnChance)
            {
                Road randomRoad = _roads[SplashKit.Rnd(_roads.Count)];
                _cars.Add(new Car(randomRoad));
            }

            // Update physics
            foreach (var car in _cars)
            {
                car.Update();
            }

            // Cleanup finished cars
            _cars.RemoveAll(c => c.IsFinished);
        }

        private void RenderGame()
        {
            SplashKit.ClearScreen(GameConfig.BackgroundColor);

            // 1. Draw Roads
            foreach (var road in _roads)
            {
                road.Draw();
            }

            // 2. Draw 'Ghost' line while drawing
            if (_isDrawing)
            {
                // Draw start point anchor
                if (_tempPoints.Count > 0) 
                    SplashKit.FillCircle(GameConfig.PreviewColor, _tempPoints[0], 5);

                // Draw line segments
                for (int i = 0; i < _tempPoints.Count - 1; i++)
                {
                    SplashKit.DrawLine(GameConfig.PreviewColor, _tempPoints[i], _tempPoints[i + 1], SplashKit.OptionLineWidth(2));
                }
            }

            // 3. Draw Cars
            foreach (var car in _cars)
            {
                car.Draw();
            }
            
            // Optional: UI / Stats
            SplashKit.DrawText($"Roads: {_roads.Count} | Cars: {_cars.Count}", Color.White, 10, 10);
        }
    }
}