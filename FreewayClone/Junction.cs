using SplashKitSDK;

namespace FreewayClone
{
    public class Junction : TrafficNode
    {
        public Junction(double x, double y, int level)
        {
            Position = new Point2D() { X = x, Y = y };
            BaseColor = Color.Gray; 
            Size = 15; 
            Id = "Junction"; 
            Level = level;
        }


        public Junction(double x, double y) : this(x, y, 0) { }

        public override void Draw()
        {
            if (Level > 0)
            {
                double shadowOffset = Level * 5;
                SplashKit.FillCircle(SplashKit.RGBAColor(0,0,0, 100), Position.X + shadowOffset, Position.Y + shadowOffset, 5);
            }
            
 
            Color junctionColor = Color.DarkGray;
            if (Level == 1) junctionColor = Color.LightGray;
            if (Level == 2) junctionColor = Color.White;

            SplashKit.FillCircle(junctionColor, Position.X, Position.Y, 5);
        }

        public override bool Contains(Point2D pt)
        {
            return SplashKit.PointPointDistance(pt, Position) <= 15;
        }

        public override Point2D GetInputPoint() => Position;
        public override Point2D GetOutputPoint() => Position;
    }
}