using SplashKitSDK;

namespace FreewayClone
{
    public abstract class TrafficNode
    {
        public Point2D Position { get; protected set; }
        public Color BaseColor { get; protected set; }
        public int Size { get; protected set; } = 40;
        public string Id { get; protected set; } = "Node";
        
        public int Level { get; set; } = 1;

        public virtual bool Contains(Point2D pt)
        {
            return pt.X >= Position.X && pt.X <= Position.X + Size &&
                   pt.Y >= Position.Y && pt.Y <= Position.Y + Size;
        }

        public abstract void Draw();

        public virtual Point2D GetInputPoint()
        {
            return new Point2D() { X = Position.X + Size / 2.0, Y = Position.Y + Size / 2.0 };
        }

        public virtual Point2D GetOutputPoint()
        {
            return new Point2D() { X = Position.X + Size / 2.0, Y = Position.Y + Size / 2.0 };
        }
    }
}