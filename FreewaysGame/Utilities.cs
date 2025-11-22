using SplashKitSDK;

namespace FreewaysGame
{
    public static class Utilities
    {
        /// <summary>
        /// Adds a Vector2D to a Point2D to get a new Point2D.
        /// Essential for calculating left/right road edges.
        /// </summary>
        public static Point2D AddVectorToPoint(Point2D p, Vector2D v)
        {
            return new Point2D() { X = p.X + v.X, Y = p.Y + v.Y };
        }

        /// <summary>
        /// Calculates a perpendicular normal vector from a direction.
        /// Used to find the width of the road relative to its forward direction.
        /// </summary>
        public static Vector2D GetNormal(Vector2D dir)
        {
            // If dir is (x, y), normal is (-y, x)
            return SplashKit.VectorTo(-dir.Y, dir.X);
        }

        /// <summary>
        /// Safe normalization that returns a zero vector if length is too small.
        /// Prevents divide-by-zero crashes.
        /// </summary>
        public static Vector2D SafeUnitVector(Vector2D v)
        {
            if (SplashKit.VectorMagnitude(v) < 0.001f) return SplashKit.VectorTo(0, 0);
            return SplashKit.UnitVector(v);
        }
    }
}