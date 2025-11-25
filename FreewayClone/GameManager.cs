using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace FreewayClone
{
    public enum GameState { Editing, Simulating, Results }

    public class GameManager
    {
        private List<TrafficNode> _nodes; 
        private List<Road> _roads;
        private List<Vehicle> _vehicles;
        
        private Road? _activeRoad = null;
        private TrafficNode? _startNode = null; 
        private const double SnapDistance = 20.0;
        
        private int _currentLevel = 1;

        public GameState State { get; private set; } = GameState.Editing;
        private int _simulationTimer = 0;
        private int _carsArrived = 0;
        private int _totalCollisions = 0;
        private int _simulationDuration = 600; 

        public GameManager()
        {
            _nodes = new List<TrafficNode>(); 
            _roads = new List<Road>(); 
            _vehicles = new List<Vehicle>();
            InitializeLevel();
        }

        private void InitializeLevel()
        {
            _nodes.Clear();
            _roads.Clear();
            _vehicles.Clear();
            
            _activeRoad = null;
            _startNode = null;
            State = GameState.Editing;
            _simulationTimer = 0;
            _carsArrived = 0;
            _totalCollisions = 0;

            var sign55 = new Sign(350, 50, Color.Blue, "55") { Level = 1 };
            var sign44 = new Sign(100, 500, Color.Red, "44") { Level = 1 };
            var sign33 = new Sign(650, 500, Color.Green, "33") { Level = 1 };

            _nodes.Add(sign55);
            _nodes.Add(sign44);
            _nodes.Add(sign33);
        }

        public void HandleInput()
        {
            Point2D mousePos = SplashKit.MousePosition();

            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                if (State == GameState.Editing)
                {
                    if (IsInRect(mousePos, 10, 10, 100, 30)) 
                    { 
                        StartSimulation(); 
                        return; 
                    }
                    if (IsInRect(mousePos, 120, 10, 80, 30)) 
                    { 
                        UndoLastRoad(); 
                        return; 
                    }
                    // Level Button
                    if (IsInRect(mousePos, 210, 10, 80, 30)) 
                    { 
                        _currentLevel++; 
                        if(_currentLevel > 3) _currentLevel = 1; 
                        return; 
                    }
                    // Restart Button
                    if (IsInRect(mousePos, 300, 10, 80, 30))
                    {
                        InitializeLevel(); 
                        return;
                    }
                }
                
                if (State != GameState.Editing && IsInRect(mousePos, 10, 10, 100, 30)) 
                { 
                    StopSimulation(); 
                    return; 
                }
                
                if (State == GameState.Results && IsInRect(mousePos, 350, 400, 100, 30)) 
                { 
                    StopSimulation(); 
                    return; 
                }
            }

            if (State == GameState.Editing)
            {
                if (SplashKit.KeyTyped(KeyCode.ZKey)) UndoLastRoad();
                if (SplashKit.KeyTyped(KeyCode.RKey)) InitializeLevel();
                if (SplashKit.KeyTyped(KeyCode.Num1Key)) _currentLevel = 1;
                if (SplashKit.KeyTyped(KeyCode.Num2Key)) _currentLevel = 2;
                if (SplashKit.KeyTyped(KeyCode.Num3Key)) _currentLevel = 3;

                if (mousePos.Y > 50)
                {
                    HandleDrawing(mousePos);
                }
            }
        }

        private void HandleDrawing(Point2D mousePos)
        {
      
            if (SplashKit.MouseDown(MouseButton.LeftButton))
            {
                if (_activeRoad == null)
                {
                    _startNode = FindAnyNodeNear(mousePos);
                    if (_startNode == null) _startNode = TrySplitRoad(mousePos, _currentLevel);
                    
                    if (_startNode == null)
                    {
                        Junction newJunction = new Junction(mousePos.X, mousePos.Y, _currentLevel);
                        _nodes.Add(newJunction);
                        _startNode = newJunction;
                    }
                    
                    if (_startNode is Junction) _currentLevel = _startNode.Level;

                    _activeRoad = new Road(_startNode, _currentLevel);
                }
                else
                {
                    if (SplashKit.PointPointDistance(mousePos, _activeRoad.Points[_activeRoad.Points.Count-1]) > 10)
                        _activeRoad.AddPoint(mousePos);
                }
            }

            
            if (SplashKit.MouseUp(MouseButton.LeftButton) && _activeRoad != null)
            {
                TrafficNode? endNode = FindAnyNodeNear(mousePos);
                
             
                if (endNode == null) 
                {
                    endNode = TrySplitRoad(mousePos, _currentLevel);
                }

                if (endNode == null)
                {
                    endNode = new Junction(mousePos.X, mousePos.Y, _currentLevel);
                    _nodes.Add(endNode);
                }

                int diff = Math.Abs(endNode.Level - _activeRoad.Level);
                
                if (endNode != _startNode && diff <= 1)
                {
                    _activeRoad.Finish(endNode);
                    _roads.Add(_activeRoad);
                }
                
                _activeRoad = null;
                _startNode = null;
            }
        }


        private void StartSimulation()
        {
            State = GameState.Simulating;
            _vehicles.Clear();
            _simulationTimer = 0;
            _carsArrived = 0;
            _totalCollisions = 0;
        }

        private void StopSimulation()
        {
            State = GameState.Editing;
            _vehicles.Clear();
        }

        private bool IsInRect(Point2D pt, double x, double y, double w, double h)
        {
            return pt.X >= x && pt.X <= x + w && pt.Y >= y && pt.Y <= y + h;
        }

        private TrafficNode? FindAnyNodeNear(Point2D pt)
        {
            foreach (var n in _nodes)
            {
                if (n is Sign && n.Contains(pt)) return n;
                if (n is Junction && SplashKit.PointPointDistance(n.Position, pt) <= SnapDistance) return n;
            }
            return null;
        }

        private TrafficNode? TrySplitRoad(Point2D pt, int level)
        {
            foreach (var road in _roads)
            {
                if (road.Level != level) continue;
                for (int i = 0; i < road.Points.Count; i++)
                {
                    if (SplashKit.PointPointDistance(road.Points[i], pt) < 15)
                    {
                        Junction splitNode = new Junction(road.Points[i].X, road.Points[i].Y, level);
                        _nodes.Add(splitNode);
                        Road r1 = new Road(road.StartNode, level);
                        r1.Finish(splitNode);
                        Road r2 = new Road(splitNode, level);
                        r2.Finish(road.EndNode);
                        _roads.Remove(road);
                        _roads.Add(r1);
                        _roads.Add(r2);
                        return splitNode;
                    }
                }
            }
            return null;
        }

        private void UndoLastRoad()
        {
            if (_roads.Count > 0)
            {
                Road r = _roads[_roads.Count - 1];
                _roads.RemoveAt(_roads.Count - 1);
                CleanupOrphanedNode(r.StartNode);
                CleanupOrphanedNode(r.EndNode);
            }
        }

        private void CleanupOrphanedNode(TrafficNode node)
        {
            if (node is Junction)
            {
                bool connected = false;
                foreach(var r in _roads) if (r.StartNode == node || r.EndNode == node) connected = true;
                if (!connected) _nodes.Remove(node);
            }
        }

        public void Update()
        {
            if (State == GameState.Results) return;

            if (State == GameState.Simulating) 
            { 
                _simulationTimer++; 
                if (_simulationTimer > _simulationDuration) State = GameState.Results; 
            }
            
            foreach(var n in _nodes) if (n is Sign s) s.Update();
            
            SpawnTraffic();
            
            UpdateVehicles();
        }
        

        private void UpdateVehicles()
        {
            for (int i = _vehicles.Count - 1; i >= 0; i--)
            {
                Vehicle v = _vehicles[i];
                v.Update(_vehicles); 
                
                if (!v.HasCrashed) 
                {
                    foreach (var o in _vehicles) 
                    {
                        if (v != o && !o.HasCrashed && v.CheckCollision(o)) 
                        { 
                            v.Crash(); 
                            o.Crash(); 
                            if (State == GameState.Simulating) _totalCollisions++; 
                        }
                    }
                }

                if (v.HasArrived)
                {
                    TrafficNode arr = v.GetArrivalNode();
                    if (arr == v.Destination && arr.Level == 1) 
                    { 
                        _vehicles.RemoveAt(i); 
                        if (State == GameState.Simulating) _carsArrived++; 
                    }
                    else if (arr is Sign) 
                    {
                        _vehicles.RemoveAt(i); 
                    }
                    else
                    {
                        Road? n = FindRoadToDestination(arr, v.Destination, v.CurrentRoad);
                        if (n != null) v.TransferToRoad(n); else _vehicles.RemoveAt(i);
                    }
                }
            }
        }

        private TrafficNode PickAlternatingDestination(TrafficNode origin)
        {
            List<TrafficNode> t = new List<TrafficNode>();
            foreach(var n in _nodes) if (n is Sign && n != origin) t.Add(n);
            if (t.Count == 0) return origin;
            _destCounter++;
            return t[Math.Abs(_destCounter) % t.Count];
        }
        private int _destCounter = 0;

        private Road? FindRoadToDestination(TrafficNode c, TrafficNode d, Road incoming)
        {
            List<Road> outgoing = new List<Road>();
            foreach(var r in _roads) 
            {
                if (r.StartNode == c)
                {
                    if (incoming != null && r.EndNode == incoming.StartNode) continue;
                    outgoing.Add(r);
                }
            }

            List<Road> valid = new List<Road>();
            foreach (var r in outgoing) 
            {
                if (CanReachDestination(r.EndNode, d)) valid.Add(r);
            }

            if (valid.Count > 0) return PickSmoothestRoad(incoming, valid);
            if (outgoing.Count > 0) return PickSmoothestRoad(incoming, outgoing);
            
            return null;
        }

        private Road PickSmoothestRoad(Road incoming, List<Road> options)
        {
            if (incoming == null || incoming.Points.Count < 2) return options[0];
            Point2D p1=incoming.Points[incoming.Points.Count-2], p2=incoming.Points[incoming.Points.Count-1];
            double a1=Math.Atan2(p2.Y-p1.Y, p2.X-p1.X);
            Road b=options[0]; double min=double.MaxValue;
            foreach(var r in options) {
                if (r.Points.Count<2) continue;
                Point2D q1=r.Points[0], q2=r.Points[1];
                double a2=Math.Atan2(q2.Y-q1.Y, q2.X-q1.X);
                double d=Math.Abs(a2-a1); if(d>Math.PI)d=2*Math.PI-d;
                if(d<min){min=d;b=r;}
            }
            return b;
        }

        private void SpawnTraffic()
        {
            Random rnd = new Random();
            foreach (var r in _roads)
            {
                if (r.StartNode is Sign s && s.TrySpawn()) 
                {
                    TrafficNode dest = PickAlternatingDestination(s);
                    
                   
                    if (CanReachDestination(r.EndNode, dest))
                    {
                        _vehicles.Add(new Vehicle(r, s.BaseColor, dest));
                    }
                }
            }
        }
        
        private bool CanReachDestination(TrafficNode start, TrafficNode target)
        {
            if (start == target) return true;
            
            
            if (start is Sign && start != target) return false;

            Queue<TrafficNode> q = new Queue<TrafficNode>();
            HashSet<TrafficNode> visited = new HashSet<TrafficNode>();

            q.Enqueue(start);
            visited.Add(start);

            while (q.Count > 0)
            {
                TrafficNode curr = q.Dequeue();
                
                if (curr == target) return true;

                
                if (curr is Sign && curr != target) continue;

                foreach (var r in _roads)
                {
                    if (r.StartNode == curr)
                    {
                        TrafficNode neighbor = r.EndNode;
                        if (!visited.Contains(neighbor))
                        {
                            visited.Add(neighbor);
                            q.Enqueue(neighbor);
                        }
                    }
                }
            }
            
            return false;
        }

        public void Draw()
        {
            // Draw World
            foreach (var r in _roads) if (r.Level == 1) r.Draw();
            foreach (var r in _roads) if (r.Level == 2) r.Draw();
            foreach (var r in _roads) if (r.Level == 3) r.Draw();
            
            if (_activeRoad != null) _activeRoad.Draw();
            foreach (var n in _nodes) n.Draw();
            foreach (var v in _vehicles) v.Draw();

            if (State == GameState.Editing)
            {
                DrawButton(10, 10, 100, "Simulate", Color.LightGreen);
                DrawButton(120, 10, 80, "Undo", Color.Orange);
                DrawButton(210, 10, 80, $"Lvl {_currentLevel}", Color.Cyan);
                DrawButton(300, 10, 80, "Restart", Color.Red);
                SplashKit.DrawText("Press 1,2,3 for Levels. 'R' Restart.", Color.White, "Arial", 14, 10, 50);
            }
            else if (State == GameState.Simulating)
            {
                DrawButton(10, 10, 100, "Stop", Color.Red);
                SplashKit.DrawText($"Time: {_simulationTimer}/{_simulationDuration}", Color.White, "Arial", 20, 130, 15);
                SplashKit.DrawText($"Collisions: {_totalCollisions}", Color.Red, "Arial", 20, 300, 15);
            }
            else if (State == GameState.Results)
            {
                SplashKit.FillRectangle(SplashKit.RGBAColor(0,0,0, 200), 100, 100, 600, 400);
                SplashKit.DrawText("Simulation Complete!", Color.White, "Arial", 30, 250, 150);
                SplashKit.DrawText($"Cars Arrived: {_carsArrived}", Color.LightGreen, "Arial", 20, 250, 220);
                SplashKit.DrawText($"Collisions: {_totalCollisions}", Color.Red, "Arial", 20, 250, 250);
                
                int efficiencyScore = Math.Max(0, 100 - (_totalCollisions * 10));
                SplashKit.DrawText($"Efficiency Score: {efficiencyScore}%", Color.Yellow, "Arial", 25, 250, 300);

                DrawButton(350, 400, 100, "Back", Color.White);
            }
        }

        private void DrawButton(double x, double y, double w, string text, Color color)
        {
            SplashKit.FillRectangle(color, x, y, w, 30);
            SplashKit.DrawRectangle(Color.Black, x, y, w, 30);
            SplashKit.DrawText(text, Color.Black, "Arial", 12, x + 10, y + 8);
        }
    }
}