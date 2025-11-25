using SplashKitSDK;

namespace FreewayClone
{
    public class Sign : TrafficNode
    {
        private int _spawnTimer = 0;
        private int _spawnRate = 60; 

        public Sign(double x, double y, Color color, string id)
        {
            Position = new Point2D() { X = x, Y = y };
            BaseColor = color;
            Id = id; 
            Size = 40; 
        }

        public void Update()
        {
            _spawnTimer++;
        }

        public bool TrySpawn()
        {
            if (_spawnTimer >= _spawnRate)
            {
                _spawnTimer = 0;
                return true;
            }
            return false;
        }

        // STRICT HITBOX: Only the stubs are clickable for drawing roads.
        public override bool Contains(Point2D pt)
        {
            // Input Stub (Left)
            if (pt.X >= Position.X - 40 && pt.X <= Position.X - 10 &&
                pt.Y >= Position.Y && pt.Y <= Position.Y + 30) return true;

            // Output Stub (Right)
            if (pt.X >= Position.X + 50 && pt.X <= Position.X + 80 &&
                pt.Y >= Position.Y && pt.Y <= Position.Y + 30) return true;

            return false;
        }

        // Point exactly at the edge of the stub, far from the sign center
        public override Point2D GetInputPoint()
        {
            return new Point2D() { X = Position.X - 40, Y = Position.Y + 12 };
        }

        public override Point2D GetOutputPoint()
        {
            return new Point2D() { X = Position.X + 80, Y = Position.Y + 12 };
        }

        public override void Draw()
        {
            // Visual Stubs (moved further out)
            // Input (Left)
            SplashKit.FillRectangle(Color.Gray, Position.X - 40, Position.Y + 2, 30, 20);
            
            // Output (Right)
            SplashKit.FillRectangle(Color.Gray, Position.X + 50, Position.Y + 2, 30, 20);

            // Sign Post & Body (Visual only, not clickable)
            SplashKit.FillRectangle(Color.Gray, Position.X + 15, Position.Y + 20, 10, 30);
            SplashKit.FillRectangle(BaseColor, Position.X, Position.Y, 40, 25);
            SplashKit.DrawRectangle(Color.Black, Position.X, Position.Y, 40, 25);
            SplashKit.DrawText(Id, Color.White, "Arial", 10, Position.X + 5, Position.Y + 8);
            
            // Indicators
            SplashKit.FillTriangle(Color.White, Position.X - 35, Position.Y + 5, Position.X - 25, Position.Y + 12, Position.X - 35, Position.Y + 19); 
            SplashKit.FillTriangle(Color.White, Position.X + 55, Position.Y + 5, Position.X + 65, Position.Y + 12, Position.X + 55, Position.Y + 19); 
        }
    }
}